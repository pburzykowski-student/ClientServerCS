using ClientServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientServer.Server
{
    class Server
    {
        private String address;
        private int port;
        private TcpListener server;

        public Server()
        {
            address = ConnectionConfig.defaultServerAddress;
            port = ConnectionConfig.defalutServerPort;
            init();
         }

        public Server(String address, int port)
        {
            this.address = address;
            this.port = port;
            init();
        }

        public void init()
        {
            server = new TcpListener(IPAddress.Parse(address), port);
            server.Start();
        }

        public void Listen()
        {
            while (true)
            {
                Console.WriteLine("Server waiting for a connection...");
                TcpClient client = server.AcceptTcpClient();
                Thread clientThread = new Thread(
                    () => HandleClient(client));
            }
        }

        private void HandleClient(TcpClient client)
        {
            Console.WriteLine("Server handle Client :: Connection detect");
        }
    }
}
