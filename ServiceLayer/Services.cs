using BusinessLogicLayer;
using System;
using System.Collections.Generic;

namespace ServiceLayer
{
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
        public static int Register(string userName, string email, string password)
        {
            try
            {
                return UserLogic.Register(userName, email, password);
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
        
        public static List<PostInformation> GetFavouritedPosts(int userId, int skipCount)
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

    }
}
