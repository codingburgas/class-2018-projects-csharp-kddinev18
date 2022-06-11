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
            bool exit = false;

            server.ServerSertUp();

            while(!exit)
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
                        Logger.ClearLogs();
                        break;
                    case "help":
                        Console.WriteLine("Command list:");
                        Console.WriteLine("    log|parameter");
                        Console.WriteLine("    stoplog");
                        Console.WriteLine("    clear");
                        Console.WriteLine("    help");
                        Console.WriteLine("    quit");
                        break;
                    case "quit":
                        exit = true;
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
