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
            Init();
         }

        public Server(String address, int port)
        {
            this.address = address;
            this.port = port;
            Init();
        }

        public void Init()
        {
            server = new TcpListener(IPAddress.Parse(address), port);
            server.Start();
        }

        public void Listen()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Server waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(
                        () => HandleClient(client));

                    clientThread.Start();
                }
            }).Start();
        }

        private void HandleClient(TcpClient client)
        {
            Console.WriteLine("Server handle Client :: Connection detect");
            ReadData(client);
            Console.WriteLine("Server handle Client :: stop reading");
        }

        private void ReadData(TcpClient client)
        {
            Console.WriteLine("Server handle Client :: ReadData");
            NetworkStream stream = client.GetStream();
            int messageSize = ReadIncomingMessageSize(stream);
            String message = ReadMessage(stream, messageSize);
            Console.WriteLine(message);

            stream.Close();
            client.Close();
        }

        private int ReadIncomingMessageSize(NetworkStream stream)
        {
            byte[] bytes = new Byte[sizeof(Int32)];
            int readed = stream.Read(bytes, 0, bytes.Length);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt32(bytes, 0);
        }

        private String ReadMessage(NetworkStream stream, int messageSize)
        {
            byte[] bytes = new Byte[messageSize];
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


            return Encoding.ASCII.GetString(bytes, 0, messageSize);
        }

        public void Dispose()
        {
            server.Stop();
        }
    }
}
