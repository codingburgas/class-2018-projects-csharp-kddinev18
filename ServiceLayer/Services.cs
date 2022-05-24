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
    public class UserCredentials
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
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
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException() { }

        public WrongCredentialsException(string message) : base(message) { }

        public WrongCredentialsException(string message, Exception inner) : base(message, inner) { }
    }
    public class NotFilledRequiredFieldsException : Exception
    {
        public NotFilledRequiredFieldsException() { }

        public NotFilledRequiredFieldsException(string message) : base(message) { }

        public NotFilledRequiredFieldsException(string message, Exception inner) : base(message, inner) { }
    }
    public class NoContentException : Exception
    {
        public NoContentException() { }

        public NoContentException(string message) : base(message) { }

        public NoContentException(string message, Exception inner) : base(message, inner) { }
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
    }
    public static class Services
    {
        private static byte[] _data = new byte[8192];
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
        public static void ResetBuffer()
        {
            Array.Clear(_data, 0, _data.Length);
        }



        public static int Register(string userName, string email, string password)
        {
            ResetBuffer();
            _tcpClient.Client.Send(Encoding.ASCII.GetBytes($"{(int)UserOperation.Register}|{userName}, {email}, {password}"));
            _tcpClient.Client.Receive(_data);
            string serialisedData = FormatData();
            if (serialisedData.Split('|')[0] == "1")
                throw new Exception(serialisedData.Split('|')[1]);

            return int.Parse(serialisedData.Split('|')[1]);
        }
        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            ResetBuffer();
            _tcpClient.Client.Send(Encoding.ASCII.GetBytes($"{(int)UserOperation.FinishRegistration}|{userId}, {gender}, {about}, {country}, {city}"));
            string serialisedData = FormatData();
            if (serialisedData.Split('|')[0] == "1")
                throw new Exception(serialisedData.Split('|')[1]);
        }
        public static int LogIn(string userName, string password, bool doRememberMe)
        {
            ResetBuffer();
            _tcpClient.Client.Send(Encoding.ASCII.GetBytes($"{(int)UserOperation.LogIn}|{userName}, {password}, {doRememberMe}"));
            _tcpClient.Client.Receive(_data);
            string serialisedData = FormatData();
            if (serialisedData.Split('|')[0] == "1")
                throw new Exception(serialisedData.Split('|')[1]);

            UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(serialisedData.Split('|')[1]);

            if (doRememberMe == true)
                AddCookies(userCredentials);
            else
                RemoveCookies();

            return userCredentials.Id;
        }
        public static int? CheckCookies()
        {
            ResetBuffer();
            if (!File.Exists(_userCredentialsPath))
                return null;

            string credentials = File.ReadAllText(_userCredentialsPath);
            UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(credentials);
            _tcpClient.Client.Send(Encoding.ASCII.GetBytes($"{(int)UserOperation.LogInWithCookies}|{userCredentials.UserName}, {userCredentials.HashedPassword}"));
            _tcpClient.Client.Receive(_data);
            string serialisedData = FormatData();
            if (serialisedData.Split('|')[0] == "1")
                throw new Exception(serialisedData.Split('|')[1]);

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
            try
            {
                List<Tuple<int, string, string, byte[], bool, bool>> posts = PostLogic.ArrangePosts(userId, skipCount);

                List<PostInformation> postsInformation = new List<PostInformation>();

                foreach (Tuple<int, string, string, byte[], bool, bool> post in posts)
                {
                    postsInformation.Add(new PostInformation()
                    {
                        PostId = post.Item1,
                        BlogName = post.Item2,
                        PostContent = post.Item3,
                        PostImage = post.Item4,
                        IsPostLiked = post.Item5,
                        IsPostFavourited = post.Item6
                    });
                }

                return postsInformation;
            }
            catch (ArgumentNullException exceprion)
            {
                throw new NoContentException(exceprion.Message);
            }

        }

        public static List<PostInformation> GetFavouritedPosts(int userId, int skipCount)
        {
            try
            {
                List<Tuple<int, string, string, byte[], bool, bool>> posts = PostLogic.ArrangeFavouritePosts(userId, skipCount);

                List<PostInformation> postsInformation = new List<PostInformation>();

                foreach (Tuple<int, string, string, byte[], bool, bool> post in posts)
                {
                    postsInformation.Add(new PostInformation()
                    {
                        PostId = post.Item1,
                        BlogName = post.Item2,
                        PostContent = post.Item3,
                        PostImage = post.Item4,
                        IsPostLiked = post.Item5,
                        IsPostFavourited = post.Item6
                    });
                }

                return postsInformation;
            }
            catch (ArgumentNullException exceprion)
            {
                throw new NoContentException(exceprion.Message);
            }
        }

        public static List<PostInformation> GetBlogPosts(int userId, int skipCount, int blodId)
        {
            try
            {
                List<Tuple<int, string, string, byte[], bool, bool>> posts = PostLogic.ArrangeBlogPosts(userId, skipCount, blodId);

                List<PostInformation> postsInformation = new List<PostInformation>();

                foreach (Tuple<int, string, string, byte[], bool, bool> post in posts)
                {
                    postsInformation.Add(new PostInformation()
                    {
                        PostId = post.Item1,
                        BlogName = post.Item2,
                        PostContent = post.Item3,
                        PostImage = post.Item4,
                        IsPostLiked = post.Item5,
                        IsPostFavourited = post.Item6
                    });
                }

                return postsInformation;
            }
            catch (ArgumentNullException exceprion)
            {
                throw new NoContentException(exceprion.Message);
            }
        }

        public static List<BlogInformation> GetBlogs(int userId)
        {
            List<Tuple<int, byte[], string, int, int>> blogs = BlogLogic.ArrangeBlogsInformation(userId);

            List<BlogInformation> blogInformation = new List<BlogInformation>();

            foreach (Tuple<int, byte[], string, int, int> blog in blogs)
            {
                blogInformation.Add(new BlogInformation()
                {
                    BlogId = blog.Item1,
                    BlogImage = blog.Item2,
                    BlogName = blog.Item3,
                    PostCount = blog.Item4,
                    FollowingCount = blog.Item5,
                });
            }

            return blogInformation;
        }

        public static List<BlogInformation> GetBlogs(string blogName)
        {
            List<Tuple<int, byte[], string, int, int>> blogs = BlogLogic.ArrangeBlogsInformation(blogName);

            List<BlogInformation> blogInformation = new List<BlogInformation>();

            foreach (Tuple<int, byte[], string, int, int> blog in blogs)
            {
                blogInformation.Add(new BlogInformation()
                {
                    BlogId = blog.Item1,
                    BlogImage = blog.Item2,
                    BlogName = blog.Item3,
                    PostCount = blog.Item4,
                    FollowingCount = blog.Item5,
                });
            }

            return blogInformation;
        }

        public static void Like(int userId, int PostId)
        {
            PostLikeLogic.Like(userId, PostId);
        }

        public static void Unlike(int userId, int PostId)
        {
            PostLikeLogic.Unlike(userId, PostId);
        }

        public static void Favourite(int userId, int PostId)
        {
            FavouritePostLogic.Favourite(userId, PostId);
        }

        public static void Unfavourite(int userId, int PostId)
        {
            FavouritePostLogic.Unfavourite(userId, PostId);
        }
    }
}
