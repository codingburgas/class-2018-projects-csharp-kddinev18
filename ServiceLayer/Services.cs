using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ServiceLayer
{
    public static class Services
    {
        private static byte[] _data = new byte[16777216];
        private static TcpClient _tcpClient;
        private readonly static string _userCredentialsPath = @$"{Directory.GetCurrentDirectory()}/DiabetesTrackerCredentials.txt";
        public static void SetUpConnection()
        {
            if(_tcpClient == null)
                _tcpClient = new TcpClient("127.0.0.1", 5400);
        }
        public static void RemoveConnection()
        {
            if(_tcpClient != null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Both);
                _tcpClient.Close();
                _tcpClient = null;
            }
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
        public static List<PostInformation> GetBlogPostsByTag(int userId, int tagId, int skipCount)
        {
            string serialisedData = ClientToServerComunication($"{(int)UserOperation.GetPostsByTag}|{userId}, {tagId}, {skipCount}");

            return JsonSerializer.Deserialize<List<PostInformation>>(serialisedData.Split('|')[1]);
        }



        public static void CreateBlog(int userId, string image, string blogName)
        {
            ClientToServerComunication($"{(int)UserOperation.CreateBlog}|{userId}, {image}, {blogName}");
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




        public static void Post(int userId, int blogId, string tags, string content, string image)
        {
            ClientToServerComunication($"{(int)UserOperation.Post}|{userId}, {blogId}, {tags}, {content}, {image}");
        }





        public static List<TagInformation> GetTags()
        {
            string serilizedResponse = ClientToServerComunication($"{(int)UserOperation.GetTags}");
            return JsonSerializer.Deserialize<List<TagInformation>>(serilizedResponse.Split('|')[1]);
        }
        public static AnalyticsInformation GetAnalyticsInformation(int userId)
        {
            string serilizedResponse = ClientToServerComunication($"{(int)UserOperation.GetAnalytics}|{userId}");
            return JsonSerializer.Deserialize<AnalyticsInformation>(serilizedResponse.Split('|')[1]);
        }



        public static void Comment(int userId, int postId, string comment)
        {
            ClientToServerComunication($"{(int)UserOperation.Comment}|{userId}, {postId}, {comment}");
        }
        public static List<string> GetComments(int userId,int postId)
        {
            string serilizedResponse = ClientToServerComunication($"{(int)UserOperation.GetComments}|{userId}, {postId}");
            return JsonSerializer.Deserialize<List<string>>(serilizedResponse.Split('|')[1]);
        }
    }
}
