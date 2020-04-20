using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User("abc","abc");
            user.CreateTopic("hello");
            user.CreateTopic("hello");
            Console.ReadLine();
        }
    }
}
