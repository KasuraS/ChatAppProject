using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Packet
    {
        public List<string> DataList;
        public DateTime TimeSent;
        public string Sender;
        public PacketType Type;

        public Packet(PacketType Type, string Sender)
        {
            DataList = new List<string>();
            this.Sender = Sender;
            this.Type = Type;
            TimeSent = DateTime.UtcNow;
        }

        public Packet(byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream ms = new MemoryStream(packetBytes);

            // Deserializes the data received and converts it into packet
            Packet p = (Packet)bf.Deserialize(ms); 
            ms.Close();

            DataList = p.DataList;
            Sender = p.Sender;
            TimeSent = p.TimeSent;
            Type = p.Type;
        }

        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream ms = new MemoryStream();
            bf.Serialize(ms, this); // Serializes the packet sent
            byte[] bytes = ((MemoryStream)ms).ToArray();
            return bytes;
        }

        public enum PacketType
        {
            ConnectToServer,
            CreateProfile,
            ConfirmProfile,
            CheckCredentials,
            AllowAccess,
            ListTopics,
            CreateTopic,
            DeleteTopic,
            JoinTopic,
            QuitTopic,
            PublicChat
        }
    }
}
