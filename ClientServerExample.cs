using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientServer
{
    class ClientServerExample
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                Server.Program serverProgram = new Server.Program();
            }).Start();

            Thread.Sleep(500);

            new Thread(() =>
            {
                Client.Program clientProgram = new Client.Program();
            }).Start();

            
            Console.ReadKey();
        }
        
    }
}
