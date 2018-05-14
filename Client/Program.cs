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

            byte[] data = Encoding.ASCII.GetBytes("test");

            Int32 size = data.Length;

            byte[] sizeOfData = BitConverter.GetBytes(size);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(sizeOfData);
            }

            client.SendData(sizeOfData);
            client.SendData(data);

        }
    }
}
