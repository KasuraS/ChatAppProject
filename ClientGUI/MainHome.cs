using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static Model.Packet;

namespace ClientGUI
{
    public partial class MainHome : Form
    {
        public int ClientID;
        public string ClientName;
        public Socket ClientSocket;

        private Button prevTopic;
        public List<Button> AllTopicButtons;

        public bool IsClosed = false;

        public MainHome(LoginForm login)
        {
            ClientID = login.ClientID;
            ClientName = login.ClientName;
            ClientSocket = login.ClientSocket;

            InitializeComponent();

            WelcomeMsg.Text = "Welcome, " + ClientName + "!";
            prevTopic = null;

            AllTopicButtons = new List<Button>();
        }
       
        // Deal with cross-thread calls
        public delegate void SafeCallDelegate1(FlowLayoutPanel panel, Button button, string action);
        public delegate void SafeCallDelegate2(RichTextBox box, string text, Color color, Font font);
        public delegate void SafeCallDelegate3(GroupBox box, FlowLayoutPanel panel);
        
        public void DoActionOnTopicsList(FlowLayoutPanel panel, Button button, string action)
        {
            // When doing some action on that component, we must call a delegate
            if (panel.InvokeRequired)
            {
                // A delegate is like a pointer as C/C++ ; it refers to this method
                var del = new SafeCallDelegate1(DoActionOnTopicsList);
                // Here we call the delegate
                Invoke(del, new object[] { panel, button, action });
            }
            else // Do tasks below, after invoking the delegate
            {
                switch (action)
                {
                    case "Create":
                        panel.Controls.Add(button); // Adds topic button
                        break;
                    case "Delete":
                        panel.Controls.Remove(button); // Removes topic button
                        break;
                    default:
                        break;
                }
            } // when we're done, we go back to do other tasks
        }

        public void WriteTextSafe(RichTextBox box, string text, Color color, Font font)
        {
            // When doing some action on that component, we must call a delegate
            if (box.InvokeRequired)
            {
                var del = new SafeCallDelegate2(WriteTextSafe); // A delegate is like a pointer as C/C++ ; it refers to this method
                // Here we call the delegate
                Invoke(del, new object[] { box, text, color, font });
            }
            else // Do tasks below, after invoking the delegate
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;
                box.SelectionColor = color; 
                box.SelectionFont = font;
                box.AppendText(text); // Changes color & font to the text & adds the text to the rest of the text
                box.SelectionColor = box.ForeColor;
            } // when we're done, we go back to do other tasks
        }

        public void DoActionOnUsersInList(FlowLayoutPanel panel, Button user, string action)
        {
            // When doing some action on that component, we must call a delegate
            if (panel.InvokeRequired)
            {
                var del = new SafeCallDelegate1(DoActionOnUsersInList); // A delegate is like a pointer as C/C++ ; it refers to this method
                // Here we call the delegate
                Invoke(del, new object[] { panel, user, action });
            }
            else // Do tasks below, after invoking the delegate
            {
                Control[] c;
                switch (action)
                {
                    case "In":
                        c = Controls.Find(user.Name, true); // Contains a button if one user button's name is there
                        if(c.Length == 0) // If not found, adds it
                            panel.Controls.Add(user);
                        break;

                    case "Out":
                        int start = user.Name.LastIndexOf('t') + 1;
                        int length = user.Name.Length - start;

                        string str = user.Name.Substring(start, length); // Retrieves user id from user button's name

                        if(ClientID == Convert.ToInt32(str))
                        {
                            panel.Controls.Clear();
                        }
                        else
                        {
                            c = Controls.Find(user.Name, true); // Contains a button if one user button's name is there
                            if (c.Length > 0) // If found, removes it
                                panel.Controls.Remove(c.First()); 
                        }
                        break;
                }
            } // when we're done, we go back to do other tasks
        }

        public void HandleSuddenDeletedTopic(GroupBox box, FlowLayoutPanel panel)
        {
            if (box.InvokeRequired)
            {
                var del = new SafeCallDelegate3(HandleSuddenDeletedTopic);
                Invoke(del, new object[] { box , panel });
            }
            else
            {
                box.Visible = false;
                panel.Controls.Clear(); // Clears chatroom users' list 
            }
        }
       
        private void MainHome_Load(object sender, EventArgs e)
        {
            Packet p = new Packet(PacketType.ListTopics, ClientName);
            p.DataList.Add(ClientID.ToString());

            ClientSocket.Send(p.ToBytes()); // Sends ListTopic-type packet to Server
        }

        private void CreateTopicButton_Click(object sender, EventArgs e)
        {
            CreateTopicForm ctf = new CreateTopicForm(this);
            ctf.Show();
        }

        private void DeleteTopicButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this topic? " +
                Environment.NewLine + "All messages will be lost / cannot be retrieved.",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                ToolStripItem b2Delete = sender as ToolStripItem;
                string Subject = b2Delete.Tag.ToString(); // TopicButton{id}
                int start = Subject.LastIndexOf('n')+1;
                int length = Subject.Length - start;
               
                int TopicID = Convert.ToInt32(Subject.Substring(start, length)); // Retrieves topic id  
               
                // Prepares the packet
                Packet p = new Packet(PacketType.DeleteTopic, ClientName);
                p.DataList.Add(TopicID.ToString());

                ClientSocket.Send(p.ToBytes()); // Sends DeleteTopic-type packet to Server
            }
        }

        private void TopicButton_Click(object sender, EventArgs e)
        {
            Button nextTopic = sender as Button;
            string Subject = nextTopic.Name;
            int start, length, TopicID;

            Packet p; 

            bool IsVisible = TopicContainer.Visible;

            if (IsVisible == true) // Quits the topic condition
            {
                if (nextTopic != prevTopic) // Joins another topic when already being in a topic
                {
                    DialogResult result = MessageBox.Show("You need first to quit a topic before joining another one.", 
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    Subject = prevTopic.Name; // TopicButton{id}
                } // If not, clicks on the same topic button to leave the topic
                start = Subject.LastIndexOf('n') + 1;
                length = Subject.Length - start;

                TopicID = Convert.ToInt32(Subject.Substring(start, length)); // Retrieves topic id

                TopicContainer.Visible = false;
                MsgTextBox.Clear();

                // Prepares the packet
                p = new Packet(PacketType.QuitTopic, ClientName);
                p.DataList.Add(TopicID.ToString());
                p.DataList.Add(ClientID.ToString());
                p.DataList.Add("Out");

                prevTopic = null; // default value for not being in a topic
            }
            else // Joins a specific topic condition
            {
                start = Subject.LastIndexOf('n') + 1;
                length = Subject.Length - start;

                TopicID = Convert.ToInt32(Subject.Substring(start, length)); // Retrieves topic id
                // All components below takes the topic id value in Tag; useful for other user actions
                TopicContainer.Tag = TopicID;
                MsgTextBox.Tag = TopicID;
                MsgTextBox.BackColor = Color.White; // Changes backcolor to white for readonly textbox

                Panel4UsersInList.Tag = TopicID;
                InputMessage.Tag = TopicID;
                SendMessageButton.Tag = TopicID;
                
                // Prepares the packet
                p = new Packet(PacketType.JoinTopic, ClientName);
                p.DataList.Add(TopicID.ToString());
                p.DataList.Add(ClientID.ToString());
                p.DataList.Add("In");

                TopicLabel.Text = nextTopic.Text;
                TopicContainer.Visible = true;

                prevTopic = sender as Button;
            }

            // Sends JoinTopic or QuitTopic-type packet to Server
            ClientSocket.Send(p.ToBytes());
        }

        private void InputMessage_TextChanged(object sender, EventArgs e)
        {
            SendMessageButton.Enabled = InputCheck();
        }

        private bool InputCheck()
        {
            string input = InputMessage.Text;
            Regex rgx = new Regex("^[a-zA-Z0-9.!?\'\"£$€]([a-zA-Z0-9,;:.!?\'\"\\-_+*/=&£$€@ ]?)+$");

            return (input.Length != 0 && rgx.IsMatch(input));
        }

        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            string Message = InputMessage.Text;
            string TopicID = b.Tag.ToString();
                
            // Prepares the packet
            Packet p = new Packet(PacketType.PublicChat, ClientName);
            p.DataList.Add(Message);
            p.DataList.Add(TopicID);
            p.DataList.Add(ClientID.ToString());
                
            ClientSocket.Send(p.ToBytes()); // Sends PublicChat-type packet to Sender

            InputMessage.Clear(); // Clears message box
        }

        // Retrieves the data, manipulates and uses it on my components
        public void DataManager(Packet p)
        {
            string TopicID, _ClientID;
            string Subject;
            string Sender = p.Sender;
            Button user;

            switch (p.Type)
            {
                case PacketType.ListTopics: // Lists Topics
                    if (p.DataList.Count > 0)
                    {
                        foreach (string tp in p.DataList)
                        {       
                            Button tpButton = new Button();

                            TopicID = tp.Split('|')[0];
                            Subject = tp.Split('|')[1];
                            string Owner = tp.Split('|')[2];

                            tpButton.Name = "TopicButton" + TopicID;
                            tpButton.Text = Subject;
                            tpButton.Width = 208;

                            tpButton.Click += TopicButton_Click; // Adds a click event for each button

                            if (Owner == ClientName) // Adds topic in Owner's list
                            {
                                tpButton.ContextMenuStrip = new ContextMenuStrip();
                                tpButton.ContextMenuStrip.Items.Add("DeleteTopicButton" + TopicID);
                                tpButton.ContextMenuStrip.Items[0].Text = "Delete Topic";
                                tpButton.ContextMenuStrip.Items[0].Enabled = true; // Can delete own topics
                                tpButton.ContextMenuStrip.Items[0].Tag = tpButton.Name;
                                tpButton.ContextMenuStrip.Items[0].Click += new EventHandler(DeleteTopicButton_Click);

                                DoActionOnTopicsList(Panel4TopicsList1, tpButton, "Create");
                            }
                            else // Adds topic in Others' list
                            {
                                DoActionOnTopicsList(Panel4TopicsList2, tpButton, "Create");
                            }

                            AllTopicButtons.Add(tpButton);
                        }
                    }
                    break;


                case PacketType.CreateTopic: // Creates a Topic button
                    switch (Convert.ToInt32(p.DataList[1]))
                    {
                        case 1:
                            TopicID = p.DataList[2];
                            Subject = p.DataList[0];

                            Button b = new Button();
                            b.Name = "TopicButton" + TopicID;
                            b.Text = Subject;
                            b.Width = 208;

                            AllTopicButtons.Add(b);
                            b.Click += TopicButton_Click; // Adds a click event for each button

                            if (Sender == ClientName) //
                            {
                                b.ContextMenuStrip = new ContextMenuStrip();
                                b.ContextMenuStrip.Items.Add("DeleteTopicButton" + TopicID);
                                b.ContextMenuStrip.Items[0].Text = "Delete Topic";
                                b.ContextMenuStrip.Items[0].Enabled = true; // Can delete own topics
                                b.ContextMenuStrip.Items[0].Tag = b.Name;
                                // Adds a menu item click event for each button
                                b.ContextMenuStrip.Items[0].Click += new EventHandler(DeleteTopicButton_Click);

                                DoActionOnTopicsList(Panel4TopicsList1, b, "Create"); // Adds topic in Owner's list

                                MessageBox.Show("Topic created.", "Confirmation",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                DoActionOnTopicsList(Panel4TopicsList2, b, "Create"); // Adds topic in Others' list
                            }
                            break;

                        default:
                            if(Sender == ClientName)  // If topic already picked; only one client can see it 
                            {
                                MessageBox.Show("Subject already picked. Please try another one!", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                    }
                    break;
                

                case PacketType.JoinTopic: // Joins a specific topic
                    _ClientID = p.DataList[1];
                    string action = p.DataList[2];

                    int start, endRange;

                    if (_ClientID != ClientID.ToString()) // Other chatters
                    {
                        start = 3;
                        endRange = p.DataList.Count;
                    }
                    else // New chatter
                    {
                        start = 4;
                        endRange = Convert.ToInt32(p.DataList[3]); // start index of the 2nd sub-list
                    }

                    while (start < endRange) // Adds chatters on the chatroom's list
                    {
                        user = new Button();
                        string client = p.DataList[start];

                        user.Name = "Client" + client.Split('|')[0];
                        user.Text = client.Split('|')[1];
                        user.BackColor = Color.White;
                        user.Width = 130;
                        user.TextAlign = ContentAlignment.MiddleLeft;
                        user.FlatStyle = FlatStyle.Flat;
                        user.FlatAppearance.BorderColor = Color.White;
                        //user.Click += PrivateChatButton_Click;

                        DoActionOnUsersInList(Panel4UsersInList, user, action);
                        start++;
                    }

                    if (_ClientID == ClientID.ToString()) // New chatter only
                    {
                        start = endRange;
                        endRange = p.DataList.Count;

                        while(start < endRange) // Adds messages on the text container
                        {
                            string Content = p.DataList[start];

                            string _TimeSent = Content.Split('|')[0];
                            string Content1 = "[" + _TimeSent + "] ";
                            WriteTextSafe(MsgTextBox, Content1, Color.Blue, new Font("Microsoft Sans Serif", 9, FontStyle.Bold));

                            string _Username = Content.Split('|')[1];
                            string Content2 = _Username + ": ";
                            WriteTextSafe(MsgTextBox, Content2, Color.Black, new Font("Microsoft Sans Serif", 9, FontStyle.Bold));

                            string _Message = Content.Split('|')[2];
                            string Content3 = _Message + Environment.NewLine;
                            WriteTextSafe(MsgTextBox, Content3, Color.Black, new Font("Microsoft Sans Serif", 9, FontStyle.Regular));
                            start++;
                        }
                    }
                    break;


                case PacketType.QuitTopic: // Leaves the topic
                    _ClientID = p.DataList[1];
                    action = p.DataList[2];

                    user = new Button();
                    user.Name = "Client" + _ClientID;
                    DoActionOnUsersInList(Panel4UsersInList, user, action); // Removes a chatter from the chatroom's list

                    break;


                case PacketType.DeleteTopic: // Deletes a specific topic
                    TopicID = p.DataList[0] as string;
       
                    Button FoundButton = null;

                    foreach (Button b2Delete in AllTopicButtons)
                    {
                        if(b2Delete.Name == "TopicButton" + TopicID)
                        {
                            FoundButton = b2Delete;

                            if (Sender == ClientName)
                            {
                                // Removes topic from Owner's list
                                DoActionOnTopicsList(Panel4TopicsList1, b2Delete, "Delete");

                                if(TopicContainer.Visible == false)
                                    MessageBox.Show("Topic deleted.", "Confirmation",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // Removes topic from Others' list
                                DoActionOnTopicsList(Panel4TopicsList2, b2Delete, "Delete");
                            }
                        }
                    }
                    // When one chatter is still in topic
                    if(TopicID == TopicContainer.Tag.ToString() && TopicContainer.Visible == true 
                        && Panel4UsersInList.Controls.Count > 0)
                    {
                        HandleSuddenDeletedTopic(TopicContainer, Panel4UsersInList);
                        MessageBox.Show("This topic does not exist or may be deleted by the Owner." + Environment.NewLine
                            + "Sorry for the inconvenience.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    AllTopicButtons.Remove(FoundButton);
                    break;


                case PacketType.PublicChat: // Sends message to all chatters in a specific topic
                    string TimeSent = p.TimeSent.ToString();
                    string Text1 = "[" + TimeSent + "] ";
                    WriteTextSafe(MsgTextBox, Text1, Color.Blue, new Font("Microsoft Sans Serif", 9, FontStyle.Bold));

                    string Username = p.Sender;
                    string Text2 = Username + ": ";
                    WriteTextSafe(MsgTextBox, Text2, Color.Black, new Font("Microsoft Sans Serif", 9, FontStyle.Bold));

                    string Message = p.DataList[0];
                    string Text3 = Message + Environment.NewLine;
                    WriteTextSafe(MsgTextBox, Text3, Color.Black, new Font("Microsoft Sans Serif", 9, FontStyle.Regular));
                    
                    break;
            }
        }

        private void MainHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // ALT+F4 or click on Close button
            {
                DialogResult result = MessageBox.Show("Are you sure to end the application?", 
                    "End Application?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    ClientSocket.Close(); // Terminate Socket; handle Socket Exception in Server side
                    IsClosed = true;
                    return;
                }
            }
            e.Cancel = true;
        }
    }
}