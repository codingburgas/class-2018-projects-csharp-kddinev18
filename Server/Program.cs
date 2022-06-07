using System;
using System.Net.Sockets;

namespace Server
{
    public class ServerApp
    {
        static void Main(string[] args)
        {
            Server server = new Server(5400);
            string userInput = String.Empty;

            server.ServerSertUp();

            while(true)
            {
                userInput = Console.ReadLine();


            }

            server.ServerShutDown();
        }
    }
}
