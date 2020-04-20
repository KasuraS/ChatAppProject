using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Model;
using static Model.Packet;

namespace ClientGUI
{
    public partial class LoginForm : Form
    {
        public int ClientID;
        public string ClientName;
        public Socket ClientSocket;
        public Thread ClientThread;

        public bool IsClosed = false;

        RegisterForm registerForm;
        MainHome main;

        public LoginForm(string host, int port)
        {
            // Defines a TCP ClientSocket
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Configures the network endpoint with localhost & port
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(host), port);
            Thread.Sleep(1000);

            // Client establishes a connection
            ClientSocket.Connect(ep);

            InitializeComponent();

            ClientThread = new Thread(DataIn);
            // Set as background thread
            ClientThread.IsBackground = true;
            ClientThread.Start();
        }

        // Deal with cross-thread calls
        public delegate void SafeCallDelegate();
        public void HideForm()
        {
            // When doing some action on that component, we must call a delegate
            if (InvokeRequired)
            {
                // A delegate is like a pointer as C/C++
                var del = new SafeCallDelegate(HideForm);
                // Here we call the delegate
                Invoke(del);
            }
            else // Do tasks below, after invoking the delegate
            {
                Hide();
            } // when we're done, we go back to do other tasks
        }

        public void DataIn()
        {
            byte[] Buffer;
            int readBytes;

            while (true)
            {
                try
                {
                    Buffer = new byte[ClientSocket.SendBufferSize]; // Gets the size of the send buffer 
                    readBytes = ClientSocket.Receive(Buffer); // Receives data from a bound Socket in memory

                    if (readBytes > 0)
                    {
                        if (registerForm != null && registerForm.Visible == true)
                        {
                            registerForm.DataManager(new Packet(Buffer));
                        }
                        else if (main != null && main.Visible == true)
                        {
                            main.DataManager(new Packet(Buffer));
                        }
                        else
                        {
                            DataManager(new Packet(Buffer)); // Deserialize here when received
                        }
                    }
                }
                catch // Handles Socket Exception
                {
                    
                    if (IsClosed == false || (registerForm != null && registerForm.IsClosed == false)
                        || (main != null && main.IsClosed == false))
                    {
                        MessageBox.Show("The server is down! We close the application.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Environment.Exit(Environment.ExitCode); // Terminates this process 
                    // Terminates ClientThread safely
                }
            }
        }

        // Retrieves the data, manipulates and uses it on my components
        public void DataManager(Packet p)
        {
            switch (p.Type)
            {
                case PacketType.ConnectToServer: // Gets a Client ID when connection to the server successful
                    ClientID = Convert.ToInt32(p.DataList[0]);
                    break;


                case PacketType.AllowAccess: // Gets a message if login is valid or not; then allows access if valid
                    switch (Convert.ToInt32(p.DataList[0]))
                    {
                        case 1:
                            MessageBox.Show("Authentification valid.", "Confirmation",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClientName = p.DataList[1];

                            HideForm(); // Using delegate to handle cross-thread calls; Hides LoginForm
                            IsClosed = true;

                            BackgroundWorker worker = new BackgroundWorker();
                            worker.WorkerReportsProgress = true;
                            // Subscribe event that does work on HandleDoWork method
                            worker.DoWork += new DoWorkEventHandler(HandleDoWork); 
                            worker.RunWorkerAsync(); // Starts execution of a background operation
                            break;

                        case 0:
                            MessageBox.Show("No user registered. Please sign up now.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        
                        default:
                            MessageBox.Show("Username and/or password incorrect.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                    break;
            }
        }
        // The second method besides delegate to handle cross-thread calls
        private void HandleDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker; 
            
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                main = new MainHome(this); // Gets access to Main Home
                main.ShowDialog(); // Shows the form as a modal dialog box
            }
        }

        private void LinkToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            IsClosed = true;
            registerForm = new RegisterForm(this);
            registerForm.Show();
            registerForm.IsClosed = false;
        }

        private void InputUserLogin_TextChanged(object sender, EventArgs e)
        {
            LoginButton.Enabled = InputCheck();
        }

        private void InputPwdLogin_TextChanged(object sender, EventArgs e)
        {
            LoginButton.Enabled = InputCheck();
        }

        private bool InputCheck()
        {
            return (InputUserLogin.Text.Length != 0 && InputPwdLogin.Text.Length != 0);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Packet p = new Packet(PacketType.CheckCredentials, "Client"); // Prepares the packet
            p.DataList.Add(ClientID.ToString());
            p.DataList.Add(InputUserLogin.Text);
            p.DataList.Add(InputPwdLogin.Text);

            ClientSocket.Send(p.ToBytes()); // Sends the CheckCredentials-type packet to Server
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
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