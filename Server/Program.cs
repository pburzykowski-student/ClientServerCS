using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ClientServer.Server
{
    class Program
    {

        public Program()
        {
            try
            {
                Server server = new Server();
                server.Listen();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

    }
}
