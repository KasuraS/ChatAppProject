using Model;
using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Model.Packet;

namespace ClientGUI
{
    public partial class CreateTopicForm : Form
    {
        public string ClientName;
        public Socket ClientSocket; 

        public CreateTopicForm(MainHome main)
        {
            InitializeComponent();
            ClientName = main.ClientName;
            ClientSocket = main.ClientSocket;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Packet p = new Packet(PacketType.CreateTopic, ClientName);
            p.DataList.Add(TopicNameInput.Text);

            ClientSocket.Send(p.ToBytes());
            Hide();
        }
        
        private void TopicNameInput_TextChanged(object sender, EventArgs e)
        {
            CreateButton.Enabled = InputCheck();
        }

        private bool InputCheck()
        {
            string input = TopicNameInput.Text;
            Regex rgx = new Regex("^([a-zA-Z0-9]{1,2}[-_ ]?[a-zA-Z0-9]{1}){1,15}[.!?]{0,3}$");

            return (input.Length != 0 && rgx.IsMatch(input));
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
