using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer.Client
{
    class Program
    {
        public Program()
        {
            Client client = new Client();
            client.Connect();
            client.SendFile("FileClient.txt");
        }
    }
}
