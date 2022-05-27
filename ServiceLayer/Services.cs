﻿using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ServiceLayer
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
        GetBlogsByName = 9,
        Like = 10,
        Unlike = 11,
        Favourite = 12,
        Unfavourite = 13,
        CheckIfBlogBelongsToUser = 14,
        Follow = 15,
        Unfollow = 16,
        GetFollowingBlogs = 17,
    }
    public class UserCredentials
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
    public class PostInformation
    {
        public int PostId { get; set; }
        public string BlogName { get; set; }
        public string PostContent { get; set; }
        public byte[] PostImage { get; set; }
        public bool IsPostLiked { get; set; }
        public bool IsPostFavourited { get; set; }
    }
    public class BlogInformation
    {
        public int BlogId { get; set; }
        public byte[] BlogImage { get; set; }
        public string BlogName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsFollowed { get; set; }
    }
    public static class Services
    {
        private static byte[] _data = new byte[16777216];
        private static TcpClient _tcpClient;
        private readonly static string _userCredentialsPath = @$"{Directory.GetCurrentDirectory()}/DiabetesTrackerCredentials.txt";
        public static void SetUpConnection()
        {
            _tcpClient = new TcpClient("127.0.0.1", 5400);
        }
        public static void RemoveConnection()
        {
            _tcpClient.Client.Shutdown(SocketShutdown.Both);
            _tcpClient.Close();
        }
        public static string FormatData()
        {
            return Encoding.ASCII.GetString(_data).Replace("\0", String.Empty);
        }
        public static void FlushBuffer()
        {
            Array.Clear(_data, 0, _data.Length);
        }
        private static string ClientToServerComunication(string message)
        {
            FlushBuffer();

            _tcpClient.Client.Send(Encoding.UTF8.GetBytes(message));
            _tcpClient.Client.Receive(_data);

            string serialisedData = FormatData();
            if (serialisedData.Split('|')[0] == "1")
                throw new Exception(serialisedData.Split('|')[1]);
            return serialisedData;
        }




        public static int Register(string userName, string email, string password)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.Register}|{userName}, {email}, {password}");

            return int.Parse(serialisedData.Split('|')[1]);
        }
        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.FinishRegistration}|{userId}, {gender}, {about}, {country}, {city}");
        }
        public static int LogIn(string userName, string password, bool doRememberMe)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.LogIn}|{userName}, {password}");

            UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(serialisedData.Split('|')[1]);

            if (doRememberMe == true)
                AddCookies(userCredentials);
            else
                RemoveCookies();

            return userCredentials.Id;
        }

        public static int? CheckCookies()
        {
            if (!File.Exists(_userCredentialsPath))
                return null;

            string credentials = File.ReadAllText(_userCredentialsPath);
            UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(credentials);
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.LogInWithCookies}|{userCredentials.UserName}, {userCredentials.HashedPassword}");
            if (int.Parse(serialisedData.Split('|')[1]) != userCredentials.Id)
                throw new Exception("Fatal error");

            return userCredentials.Id;
        }
        public static void RemoveCookies()
        {
            if (!File.Exists(_userCredentialsPath))
                return;

            File.Delete(_userCredentialsPath);
        }
        public static int? LogInWithCookies()
        {
            int? userId = CheckCookies();
            if (userId is null)
            {
                return null;
            }

            return userId.Value;
        }
        public static void AddCookies(UserCredentials userCredentials)
        {
            File.WriteAllText(_userCredentialsPath, JsonSerializer.Serialize(new UserCredentials() { Id = userCredentials.Id, UserName = userCredentials.UserName, HashedPassword = userCredentials.HashedPassword }));
        }





        public static List<PostInformation> GetPosts(int userId, int skipCount)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.GetPosts}|{userId}, {skipCount}");

            return JsonSerializer.Deserialize<List<PostInformation>>(serialisedData.Split('|')[1]);
        }
        public static List<PostInformation> GetFavouritedPosts(int userId, int skipCount)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.GetFavouritedPosts}|{userId}, {skipCount}");

            return JsonSerializer.Deserialize<List<PostInformation>>(serialisedData.Split('|')[1]);
        }
        public static List<PostInformation> GetBlogPosts(int userId, int skipCount, int blodId)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.GetBlogPosts}|{userId}, {skipCount}, {blodId}");

            return JsonSerializer.Deserialize<List<PostInformation>>(serialisedData.Split('|')[1]);
        }





        public static List<BlogInformation> GetBlogs(int userId)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.GetBlogs}|{userId}");

            return JsonSerializer.Deserialize<List<BlogInformation>>(serialisedData.Split("|")[1]);
        }
        public static List<BlogInformation> GetBlogs(int userId, string blogName)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.GetBlogsByName}|{userId}, {blogName}");

            return JsonSerializer.Deserialize<List<BlogInformation>>(serialisedData.Split("|")[1]);
        }
        public static List<BlogInformation> GetFollowingBlogs(int userId)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.GetFollowingBlogs}|{userId}");

            return JsonSerializer.Deserialize<List<BlogInformation>>(serialisedData.Split("|")[1]);
        }
        public static bool BelongsToUser(int userId, int blogId)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.CheckIfBlogBelongsToUser}|{userId}, {blogId}");

            return bool.Parse(serialisedData.Split('|')[1]);
        }





        public static void Like(int userId, int PostId)
        {
            ClientToServerComunication($"{(int)UserOperation.Like}|{userId}, {PostId}");
        }
        public static void Unlike(int userId, int PostId)
        {
            ClientToServerComunication($"{(int)UserOperation.Unlike}|{userId}, {PostId}");
        }
        public static void Favourite(int userId, int PostId)
        {
            ClientToServerComunication($"{(int)UserOperation.Favourite}|{userId}, {PostId}");
        }
        public static void Unfavourite(int userId, int PostId)
        {
            ClientToServerComunication($"{(int)UserOperation.Unfavourite}|{userId}, {PostId}");
        }
        public static void Follow(int userId, int blogId)
        {
            ClientToServerComunication($"{(int)UserOperation.Follow}|{userId}, {blogId}");
        }
        public static void Unfollow(int userId, int blogId)
        {
            ClientToServerComunication($"{(int)UserOperation.Unfollow}|{userId}, {blogId}");
        }
    }
}
