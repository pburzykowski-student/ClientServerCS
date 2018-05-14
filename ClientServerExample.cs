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
            Server.Program serverProgram = new Server.Program();
            Client.Program clientProgram = new Client.Program();
            //Client.Program clientProgram2 = new Client.Program();

            Console.ReadKey();
        }
        
    }
}
