using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Message
    {
        public string Sender { get; private set; }
        public string Content { get; private set; }
        public string TimeSent { get; private set; }

        public Message(string Sender, string Content, string TimeSent)
        {
            this.Sender = Sender;
            this.Content = Content;
            this.TimeSent = TimeSent;
        }

        public override string ToString()
        {
            return TimeSent + "|" + Sender + "|" + Content;
        }
    }

    interface IMessages
    {
        void CreateBackUp(int TopicID);
        void StoreMessage(Message msg, int TopicID);
        List<Message> GetMessages(int TopicID);
        void DeleteMessages(int TopicID);
    }

    public class MessagesManager : IMessages
    {
        private string FilePath = "C:\\ChatAppData\\public\\Topic";
        private List<Message> MessagesList;

        public void CreateBackUp(int TopicID)
        {
            if (!Directory.Exists("C:\\ChatAppData\\public")) // When the directory does not exist
                Directory.CreateDirectory("C:\\ChatAppData\\public"); // Creates the directory

            FilePath += TopicID + ".txt";

            // If exists, opens the file; otherwise creates it
            FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate);
            fs.Close();
        }

        public void StoreMessage(Message msg, int TopicID)
        {
            string NewLine = msg.ToString() + Environment.NewLine;

            CreateBackUp(TopicID);

            File.AppendAllText(FilePath, NewLine); // Adds the new line in the existing file
        }

        public List<Message> GetMessages(int TopicID)
        {
            MessagesList = new List<Message>();
            FilePath += TopicID + ".txt";

            FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read);

            using (StreamReader sr = new StreamReader(fs)) // Reads characters from a byte stream
            {
                string s;

                while ((s = sr.ReadLine()) != null) // Reads the file line by line
                {
                    string TimeSent = s.Split('|')[0]; 
                    string Sender = s.Split('|')[1]; 
                    string Content = s.Split('|')[2];
                    Message msg = new Message(Sender, Content, TimeSent);
                    MessagesList.Add(msg);
                }
            }
            fs.Close();

            return MessagesList;
        }

        public void DeleteMessages(int TopicID)
        {
            FilePath += TopicID + ".txt";

            if (!File.Exists(FilePath)) // When the file does not exist
                return;

            // When the file does exist
            File.Delete(FilePath);
        }
    }
}
