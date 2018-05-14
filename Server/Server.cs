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
    class Server : IDisposable
    {
        private String address;
        private int port;
        private TcpListener server;

        private const int MESSAGE_SIZE = 256;
        private const int BUFFOR_SIZE = 32;

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

                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            Console.WriteLine("Server handle Client :: Connection detect");
            readData(client);
            Console.WriteLine("Server handle Client :: stop reading");
        }

        private void readData(TcpClient client)
        {
            Console.WriteLine("Server handle Client :: ReadData");
            NetworkStream stream = client.GetStream();
            int messageSize = readIncomingMessageSize(stream);            

            byte [] bytes = new Byte[messageSize];
            int readed = 0;

            do
            {
                try
                {
                    readed += stream.Read(bytes, readed, messageSize - readed);
                }
                catch (System.IO.IOException e)
                {
                    break;
                }
            } while (readed != messageSize);


            Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, messageSize));

            stream.Close();
            client.Close();
        }

        private int readIncomingMessageSize(NetworkStream stream)
        {
            byte[] bytes = new Byte[sizeof(Int32)];
            int readed = stream.Read(bytes, 0, bytes.Length);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt32(bytes, 0);
        }


        public void Dispose()
        {
            server.Stop();
        }
    }
}
