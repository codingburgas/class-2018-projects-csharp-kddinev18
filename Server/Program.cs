using System;
using System.Net.Sockets;

namespace Server
{
    public class ServerApp
    {
        static void Main(string[] args)
        {
            Server server = new Server(5400);

            server.ServerSertUp();

            Console.Read();

            server.ServerShutDown();
        }
    }
}
