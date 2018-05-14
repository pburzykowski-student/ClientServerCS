using ClientServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer.Client
{

    class Client : IDisposable
    {
        private String serverAddress;
        private int serverPort;
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public Client()
        {
            this.serverAddress = ConnectionConfig.defaultServerAddress;
            this.serverPort = ConnectionConfig.defalutServerPort;
            tcpClient = new TcpClient();
        }

        public Client(String serverAddress, int serverPort)
        {
            this.serverAddress = serverAddress;
            this.serverPort = serverPort;
            tcpClient = new TcpClient();
        }

        public void Connect()
        {
            try
            {
                Console.WriteLine("Client connecting to server");
                tcpClient.Connect(serverAddress, serverPort);
                networkStream = tcpClient.GetStream();
            }
            catch(SocketException e)
            {
                Console.Error.WriteLine("Connection socket error\n" + e.Message);
                Console.ReadKey();
                Environment.Exit(-1);
            }
        }

        public void SendData(byte[] data)
        {
            try
            {
                networkStream.Write(data, 0, data.Length);
            } catch(SocketException e)
            {
                Console.Error.WriteLine("Writing to server socket error\n" + e.Message);
                Console.ReadKey();
            }
        }

        public void Dispose()
        {
            networkStream.Close();
            tcpClient.Close();
        }

    }
}
