using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        
        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public override string ToString()
        {
            return Username + "|" + Password;
        }
    }


    interface IUsers
    {
        int CreateUser(User user);
        int AuthUser(User user);
    }

    public class UsersManager : IUsers
    {
        private readonly string FilePath = "C:\\ChatAppData\\info\\UsersList.txt";

        public int CreateUser(User user)
        {
            string NewLine = user.ToString() + Environment.NewLine;

            if (!Directory.Exists("C:\\ChatAppData\\info")) // When the directory does not exist
                Directory.CreateDirectory("C:\\ChatAppData\\info"); // Creates the directory

            if (!File.Exists(FilePath)) // When the file does not exist
            {    
                File.WriteAllText(FilePath, NewLine); // Creates the file and writes the new line
                return 1; 
            }

            // When the file does exist
            using(StreamReader sr = File.OpenText(FilePath)) // Reads characters from a byte stream
            {
                string s;
                while((s = sr.ReadLine()) != null) // Reads the file line by line
                {
                    string UserRegistered = s.Split('|')[0]; // Gets username in the file
                    // Finds this username is already used
                    if (UserRegistered == user.Username) return 0; 
                }
            }

            // This username can be used
            File.AppendAllText(FilePath, NewLine); // Adds the new line in the existing file
            return 1;
        }

        public int AuthUser(User user)
        {
            if (!File.Exists(FilePath)) return 0; // When no registration, user must register
 
            // When the file does exist
            using (StreamReader sr = File.OpenText(FilePath)) // Reads characters from a byte stream
            {
                string s;
                while ((s = sr.ReadLine()) != null) // Reads the file line by line
                {
                    string UserRegistered = s.Split('|')[0]; // Gets username in the file
                    string PwdRegistered = s.Split('|')[1]; // Gets password
                    // Authentification valid
                    if (UserRegistered == user.Username & PwdRegistered == user.Password) return 1;
                }
            }

            // Username & password incorrect
            return -1;
        }
    }

}
