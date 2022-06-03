﻿using BusinessLogicLayer;
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
        GetPostsByTag = 8,
        GetBlogs = 9,
        GetBlogsByName = 10,
        Like = 11,
        Unlike = 12,
        Favourite = 13,
        Unfavourite = 14,
        CheckIfBlogBelongsToUser = 15,
        Follow = 16,
        Unfollow = 17,
        GetFollowingBlogs = 18,
        Post = 19,
        GetTags = 20,
        GetAnalytics = 21,
        Comment = 22,
        GetComments = 23,
        CreateBlog = 24
    }
    public class Server
    {
        private static TcpListener _tcpListener;
        private static List<TcpClient> _clients = new List<TcpClient>();
        private static byte[] _data = new byte[16777216];
        private int _port;
        private static int _success = 0;
        private static int _error = 1;

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
            client.Client.BeginReceive(_data, 0, _data.Length, SocketFlags.None, new AsyncCallback(ReciveUserInput), client);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptClients), null);
        }

        public static void ReciveUserInput(IAsyncResult asyncResult)
        {
            TcpClient client = asyncResult.AsyncState as TcpClient;
            int reciever;
            List<string> args;
            try
            {
                reciever = client.Client.EndReceive(asyncResult);
                if (reciever == 0)
                {
                    DisconnectClient(client);
                    return;
                }
                string data = Encoding.ASCII.GetString(_data).Replace("\0", String.Empty);
                try
                {
                    args = data.Split('|')[1].Split(", ").ToList();
                }
                catch (Exception)
                {
                    args = null;
                }
                SendCorrenspodingResponse(client, int.Parse(data.Split('|')[0]), args);
                FlushBuffer();
            }
            catch (Exception ex)
            {
                string response = $"{_error}|{ex.Message}";
                client.Client.Send(Encoding.ASCII.GetBytes(response));
            }
            client.Client.BeginReceive(_data, 0, _data.Length, SocketFlags.None, new AsyncCallback(ReciveUserInput), client);
        }

        public static void FlushBuffer()
        {
            Array.Clear(_data, 0, _data.Length);
        }

        public static void DisconnectClient(TcpClient client)
        {
            client.Client.Shutdown(SocketShutdown.Both);
            client.Client.Close();
            _clients.Remove(client);
        }

        public static void SendCorrenspodingResponse(TcpClient client, int operationNumber, List<string> args)
        {
            UserOperation operation = (UserOperation)operationNumber;
            string response = String.Empty;
            switch (operation)
            {
                case UserOperation.Register:
                    int userId = Operations.Register(args[0], args[1], args[2]);
                    response = $"{_success}|{userId}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.FinishRegistration:
                    Operations.FinishRegistration(int.Parse(args[0]), args[1].ToCharArray()[0], args[2], args[3], args[4]);
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.LogIn:
                    response = $"{_success}|{Operations.LogIn(args[0], args[1])}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.LogInWithCookies:
                    response = $"{_success}|{Operations.LogInWithCookies(args[0], args[1])}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetPosts:
                    response = $"{_success}|{Operations.GetPosts(int.Parse(args[0]), int.Parse(args[1]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetFavouritedPosts:
                    response = $"{_success}|{Operations.GetFavouritePosts(int.Parse(args[0]), int.Parse(args[1]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetBlogPosts:
                    response = $"{_success}|{Operations.GetBlogPosts(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetPostsByTag:
                    response = $"{_success}|{Operations.GetPostsByTag(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetBlogs:
                    response = $"{_success}|{Operations.GetBlogs(int.Parse(args[0]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetBlogsByName:
                    response = $"{_success}|{Operations.GetBlogs(int.Parse(args[0]), args[1])}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Like:
                    Operations.Like(int.Parse(args[0]), int.Parse(args[1]));
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Unlike:
                    Operations.Unlike(int.Parse(args[0]), int.Parse(args[1]));
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Favourite:
                    Operations.Favourite(int.Parse(args[0]), int.Parse(args[1]));
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Unfavourite:
                    Operations.Unfavourite(int.Parse(args[0]), int.Parse(args[1]));
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.CheckIfBlogBelongsToUser:
                    response = $"{_success}|{Operations.BelogsToUSer(int.Parse(args[0]), int.Parse(args[1]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Follow:
                    Operations.Follow(int.Parse(args[0]), int.Parse(args[1]));
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Unfollow:
                    Operations.Unfollow(int.Parse(args[0]), int.Parse(args[1]));
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetFollowingBlogs:
                    response = $"{_success}|{Operations.GetFollowingBlogs(int.Parse(args[0]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Post:
                    Operations.Post(int.Parse(args[0]), int.Parse(args[1]), args[2], args[3], args[4]);
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetTags:
                    response = $"{_success}|{Operations.GetTags()}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetAnalytics:
                    response = $"{_success}|{Operations.GetAnalytics(int.Parse(args[0]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.Comment:
                    Operations.Comment(int.Parse(args[0]), int.Parse(args[1]), args[2]);
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.GetComments:
                    response = $"{_success}|{Operations.GetComments(int.Parse(args[0]), int.Parse(args[1]))}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                case UserOperation.CreateBlog:
                    Operations.CreateBlog(int.Parse(args[0]), args[1], args[2]);
                    response = $"{_success}";
                    client.Client.Send(Encoding.UTF8.GetBytes(response));
                    break;
                default:
                    break;
            }
        }
    }
}
