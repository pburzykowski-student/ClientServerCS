using ClientServer.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
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

        private void SendData(byte[] data)
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

        public void SendMessage(String message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            Int32 size = data.Length;

            byte[] sizeOfData = BitConverter.GetBytes(size);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(sizeOfData);
            }

            SendData(sizeOfData);
            SendData(data);
        }

        public void SendFile(String filename)
        {

            byte[] data = System.IO.File.ReadAllBytes("..\\..\\Client\\" + filename);
            Int32 size = data.Length;

            byte[] sizeOfData = BitConverter.GetBytes(size);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(sizeOfData);
            }

            SendData(sizeOfData);
            SendData(data);
        }

        public void Dispose()
        {
            networkStream.Close();
            tcpClient.Close();
        }

    }
}
