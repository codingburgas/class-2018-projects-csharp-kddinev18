using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        private static TcpClient _tcpClient;
        public static void SetUpConnection()
        {
            _tcpClient = new TcpClient("127.0.0.1", 5400);
        }
        public static void RemoveConnection()
        {
            _tcpClient.Client.Shutdown(SocketShutdown.Both);
            _tcpClient.Close();
        }

        public static int Register(string userName, string email, string password)
        {
            byte[] data = new byte[8192];
            try
            {
                _tcpClient.Client.Send(Encoding.ASCII.GetBytes($"{(int)UserOperation.Register}|{userName}, {email}, {password}"));
                _tcpClient.Client.Receive(data);
                string serialisedData = Encoding.ASCII.GetString(data);
                return int.Parse(serialisedData);
                //return UserLogic.Register(userName, email, password);
            }
            catch (ArgumentException exception)
            {
                throw new WrongCredentialsException(exception.Message);
            }
            catch (Exception)
            {
                throw new Exception("Fatal error!");
            }
        }
        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            try
            {
                UserProfileLogic.ConfigureUserProfile(userId, gender, about, country, city);
            }
            catch (ArgumentNullException exception)
            {
                throw new NotFilledRequiredFieldsException(exception.Message);
            }
            catch (ArgumentException exception)
            {
                throw new WrongCredentialsException(exception.Message);
            }
            catch (Exception)
            {
                throw new Exception("Fatal error!");
            }
        }

        public static int LogIn(string userName, string password, bool doRememberMe)
        {
            try
            {
                return UserLogic.LogIn(userName, password, doRememberMe);
            }
            catch (ArgumentNullException exception)
            {
                throw new NotFilledRequiredFieldsException(exception.Message);
            }
            catch (ArgumentException exception)
            {
                throw new WrongCredentialsException(exception.Message);
            }
            catch (Exception)
            {
                throw new Exception("Fatal error!");
            }
        }

        public static int? LogInWithCookies()
        {
            int? userId = UserLogic.CheckCookies();
            if (userId is null)
            {
                return null;
            }

            return userId.Value;
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
