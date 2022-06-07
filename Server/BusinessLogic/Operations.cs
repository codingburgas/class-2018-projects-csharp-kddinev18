using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    public static class Operations
    {
        public static int Register(string userName, string email, string password)
        {
            int userId = UserLogic.Register(userName, email, password);
            Logger.WriteData(2, "Information", "Register");

            return userId;
        }

        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            UserProfileLogic.ConfigureUserProfile(userId, gender, about, country, city);

            Logger.WriteData(2, "Information", "FinishRegistration");
        }

        public static string LogIn(string userName, string password)
        {
            string serializedResponse = JsonSerializer.Serialize(UserLogic.LogIn(userName, password));
            Logger.WriteData(2, "Information", "LogIn");

            return serializedResponse;
        }

        public static int LogInWithCookies(string userName, string password)
        {
            int userId = UserLogic.LogInWithPreHashedPassword(userName, password);
            Logger.WriteData(2, "Information", "LogIn");

            return userId;
        }

        public static string GetPosts(int userId, int skipCount)
        {
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangePosts(userId, skipCount));
            Logger.WriteData(2, "Information", "GetPosts");

            return serializedResponse;
        }

        public static string GetFavouritePosts(int userId, int skipCount)
        {
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangeFavouritePosts(userId, skipCount));
            Logger.WriteData(2, "Information", "GetFavouritePosts");

            return serializedResponse;
        }

        public static string GetBlogPosts(int userId, int skipCount, int blogId)
        {
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangeBlogPosts(userId, skipCount, blogId));
            Logger.WriteData(2, "Information", "GetBlogPosts");

            return serializedResponse;
        }

        public static string GetBlogs(int userId)
        {
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId));
            Logger.WriteData(2, "Information", "GetBlogs");

            return serializedResponse;
        }

        public static string GetBlogs(int userId, string blogName)
        {
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId, blogName));
            Logger.WriteData(2, "Information", "GetBlogs");

            return serializedResponse;
        }

        public static void Like(int userId, int postId)
        {
            PostLikeLogic.Like(userId, postId);
            Logger.WriteData(2, "Information", "Like");
        }

        public static void Unlike(int userId, int postId)
        {
            PostLikeLogic.Unlike(userId, postId);
            Logger.WriteData(2, "Information", "Unlike");

        }

        public static void Favourite(int userId, int postId)
        {
            FavouritePostLogic.Favourite(userId, postId);
            Logger.WriteData(2, "Information", "Favourite");

        }

        public static void Unfavourite(int userId, int postId)
        {
            FavouritePostLogic.Unfavourite(userId, postId);
            Logger.WriteData(2, "Information", "Unfavourite");

        }

        public static string BelogsToUser(int userId, int postId)
        {
            string serializedResponse = BlogLogic.BelogsToUser(userId, postId).ToString();
            Logger.WriteData(2, "Information", "BelogsToUser");
            return serializedResponse;
        }

        public static void Follow(int userId, int blogId)
        {
            FollowingBlogLogic.Follow(userId, blogId);
            Logger.WriteData(2, "Information", "Follow");

        }

        public static void Unfollow(int userId, int blogId)
        {
            FollowingBlogLogic.Unfollow(userId, blogId);
            Logger.WriteData(2, "Information", "Unfollow");

        }

        public static string GetFollowingBlogs(int userId)
        {
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeFollowedBlogInforamtion(userId));
            Logger.WriteData(2, "Information", "GetFollowingBlogs");

            return serializedResponse;
        }

        public  static void Post(int blogId, int userId, string tags, string content, string image)
        {
            string[] stringBytes = image.Split(';');
            byte[] imageBytes = new byte[stringBytes.Length];
            for (int i = 0; i < imageBytes.Length; i++)
            {
                imageBytes[i] = byte.Parse(stringBytes[i]);
            }
            PostLogic.AddPost(blogId, tags.Split(';'), content, imageBytes, userId);
            Logger.WriteData(2, "Information", "Post");

        }

        public static string GetTags()
        {
            string serializedResponse = JsonSerializer.Serialize(TagLogic.ArraingeTags());
            Logger.WriteData(2, "Information", "GetTags");

            return serializedResponse;
        }

        public static string GetAnalytics(int userId)
        {
            string serializedResponse = JsonSerializer.Serialize(UserProfileLogic.ArrangeAnalyticsInformation(userId));
            Logger.WriteData(2, "Information", "GetAnalytics");

            return serializedResponse;
        }

        public static string GetPostsByTag(int userId, int tagId, int skipCount)
        {
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangePostByTag(userId, tagId, skipCount));
            Logger.WriteData(2, "Information", "GetPostsByTag");

            return serializedResponse;
        }

        public static void Comment(int userId, int postId, string comment)
        {
            PostCommentLogic.Comment(userId, postId, comment);
            Logger.WriteData(2, "Information", "Comment");

        }

        public static string GetComments(int userId, int postId)
        {
            string serializedResponse = JsonSerializer.Serialize(PostCommentLogic.GetComments(userId, postId));
            Logger.WriteData(2, "Information", "GetComments");

            return serializedResponse;
        }

        public static void CreateBlog(int userId, string image, string blogName)
        {
            string[] stringBytes = image.Split(';');
            byte[] imageBytes = new byte[stringBytes.Length];
            for (int i = 0; i < imageBytes.Length; i++)
            {
                imageBytes[i] = byte.Parse(stringBytes[i]);
            }
            BlogLogic.CreateBlog(userId, blogName, imageBytes);
            Logger.WriteData(2, "Information", "CreateBlog");

        }
    }
}
