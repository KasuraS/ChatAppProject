using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Topic
    {
        public int TopicID { get; private set; }
        public string Subject { get; private set; }
        public string Owner { get; private set; }

        public Topic(string Subject, string Owner) 
        {
            TopicsManager tm = new TopicsManager();
            TopicID = tm.GetValidTopicID();
            this.Subject = Subject;
            this.Owner = Owner;
        }

        public Topic(int TopicID, string Subject, string Owner)
        {
            this.TopicID = TopicID;
            this.Subject = Subject;
            this.Owner = Owner;
        }

        public override string ToString()
        {
            return TopicID + "|" + Subject + "|" + Owner;
        }
    }

    interface ITopics
    {
        int CreateTopic(Topic topic);
        int GetValidTopicID();
        List<Topic> GetTopics();
        void DeleteTopic(int id);
    }

    public class TopicsManager : ITopics
    {
        private readonly string FilePath = "C:\\ChatAppData\\info\\TopicsList.txt";
        private List<Topic> TopicsList;

        public int CreateTopic(Topic topic)
        {
            string NewLine = topic.ToString() + Environment.NewLine;

            if (!Directory.Exists("C:\\ChatAppData\\info")) // When the directory does not exist
                Directory.CreateDirectory("C:\\ChatAppData\\info"); // Creates the directory

            if (!File.Exists(FilePath)) // When the file does not exist
            {
                File.WriteAllText(FilePath, NewLine); // Creates the file and writes the new line
                return 1;
            }

            // When the file does exist
            using (StreamReader sr = File.OpenText(FilePath)) // Reads characters from a byte stream
            {
                string s;
                while ((s = sr.ReadLine()) != null) // Reads the file line by line
                {
                    string SubjectRegistered = s.Split('|')[1]; // Gets topic name in the file
                    // Finds this topic name is already used by another user
                    if (SubjectRegistered == topic.Subject) return 0;
                }
            }

            // This topic name is not used yet by this user
            File.AppendAllText(FilePath, NewLine); // Adds the new line in the existing file
            return 1;
        }

        public int GetValidTopicID()
        {
            if (!File.Exists(FilePath)) // When the file does not exist, id = 1
                return 1;

            // When the file does exist
            using (StreamReader sr = File.OpenText(FilePath))
            {
                string s;
                string TopicIDRegistered = null;
                while ((s = sr.ReadLine()) != null) // Reads the file line by line
                {
                    TopicIDRegistered = s.Split('|')[0]; 
                }

                if (TopicIDRegistered == null) return 1; // If empty file, id = 1
                
                // If not empty, id = last id in the file + 1
                int LastTopicID = Convert.ToInt32(TopicIDRegistered) + 1; 
                return LastTopicID; 
            }
        }

        public void DeleteTopic(int TopicToDeleteID)
        {
            var tempFile = Path.GetTempFileName();
            // Create a request LINQ for reading all lines that TopicID != TopicToDeleteID
            var linesToKeep = File.ReadLines(FilePath).Where(l => l.Split('|')[0] != TopicToDeleteID.ToString());
           
            File.WriteAllLines(tempFile, linesToKeep); // Writes all lines to temp file
            File.Delete(FilePath); // Deletes the current file
            File.Move(tempFile, FilePath); // Moves the temp file to the indicated path
        }

        public List<Topic> GetTopics()
        {
            TopicsList = new List<Topic>();
           
            if (!Directory.Exists("C:\\ChatAppData\\info")) // When the directory does not exist
                Directory.CreateDirectory("C:\\ChatAppData\\info"); // Creates the directory
            // If exists, opens the file; otherwise creates it
            FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read);

            using (StreamReader sr = new StreamReader(fs))  // Reads characters from a byte stream
            { 
                string s;

                    while ((s = sr.ReadLine()) != null) // Reads the file line by line
                    {
                        int TopicIDRegistered = Convert.ToInt32(s.Split('|')[0]); 
                        string SubjectRegistered = s.Split('|')[1]; 
                        string OwnerRegistered = s.Split('|')[2]; 

                        Topic topic = new Topic(TopicIDRegistered, SubjectRegistered, OwnerRegistered);

                        TopicsList.Add(topic);
                    }
            }
            fs.Close();

            return TopicsList;
        }
    }
}
