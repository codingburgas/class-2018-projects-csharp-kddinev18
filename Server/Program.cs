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
                switch (userInput.Split('|')[0])
                {
                    case "log":
                        try
                        {
                            int parameter = int.Parse(userInput.Split('|')[1]);
                            Logger.ConfigLogger(parameter);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wrong input. Input format must be: log|parameter");
                        }
                        break;
                    case "stoplog":
                        Logger.SeverityToLog = null;
                        break;
                    case "clear":

                        break;
                    case "help":
                        break;
                    case "quit":
                        break;
                    default:
                        Console.WriteLine("Wrong input. Input format must be: command|parameter");
                        break;
                }


            }

            server.ServerShutDown();
        }
    }
}
