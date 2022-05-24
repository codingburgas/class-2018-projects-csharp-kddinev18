using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    enum UserOperation
    {
        Register = 0,
        FinishRegistration = 1,
        LogIn = 2,
        LogInWithCookies = 3,
        GetPosts = 4,
        GetFavouritedPosts = 5,
        GetBlogPosts = 7,
        GetBlogs = 8,
        Like = 9,
        Unlike = 10,
        Favourite = 11,
        Unfavourite = 12,
    }
    public class Server
    {
        private static TcpListener _tcpListener;
        private static List<TcpClient> _clients = new List<TcpClient>();
        private static byte[] _data = new byte[1024];
        private int _port;
        public Server(int port)
        {
            _port = port;
        }

        public void ServerSertUp()
        {
            Master.OpenConnection();
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptClients), null);
        }

        public static void AcceptClients(IAsyncResult asyncResult)
        {
            TcpClient client;
            try
            {
                client = _tcpListener.EndAcceptTcpClient(asyncResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            _clients.Add(client);
            //client.Client.Send(Encoding.ASCII.GetBytes("HI"));
            client.Client.BeginReceive(_data, 0, _data.Length, SocketFlags.None, new AsyncCallback(ReciveUserInput), client);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptClients), null);
        }

        public static void ReciveUserInput(IAsyncResult asyncResult)
        {
            TcpClient client = asyncResult.AsyncState as TcpClient;
            int reciever;
            try
            {
                reciever = client.Client.EndReceive(asyncResult);
                if (reciever == 0)
                {
                    DisconnectClient(client);
                    return;
                }
                string data = Encoding.ASCII.GetString(_data).Replace("\0", String.Empty);
                SendCorrenspodingResponse(client, int.Parse(data.Split('|')[0]), data.Split('|')[1].Split(", "));
            }
            catch (Exception ex)
            {
                DisconnectClient(client);
                throw;
            }
            client.Client.BeginReceive(_data, 0, _data.Length, SocketFlags.None, new AsyncCallback(ReciveUserInput), client);
        }

        public static void DisconnectClient(TcpClient client)
        {
            client.Client.Shutdown(SocketShutdown.Both);
            client.Client.Close();
            _clients.Remove(client);
        }

        public static void SendCorrenspodingResponse(TcpClient client, int operationNumber, string[] args)
        {
            UserOperation operation = (UserOperation)operationNumber;

            switch (operation)
            {
                case UserOperation.Register:
                    int userId = Operations.Register(args[0], args[1], args[2]);
                    client.Client.Send(Encoding.ASCII.GetBytes(userId.ToString()));
                    break;
                case UserOperation.FinishRegistration:
                    break;
                case UserOperation.LogIn:
                    break;
                case UserOperation.LogInWithCookies:
                    break;
                case UserOperation.GetPosts:
                    break;
                case UserOperation.GetFavouritedPosts:
                    break;
                case UserOperation.GetBlogPosts:
                    break;
                case UserOperation.GetBlogs:
                    break;
                case UserOperation.Like:
                    break;
                case UserOperation.Unlike:
                    break;
                case UserOperation.Favourite:
                    break;
                case UserOperation.Unfavourite:
                    break;
                default:
                    break;
            }
        }
    }
}
