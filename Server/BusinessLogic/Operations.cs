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
            Logger.WriteData(2, "Information", $"Register - UserId: {userId}, UserName: {userName}");

            return userId;
        }

        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            UserProfileLogic.ConfigureUserProfile(userId, gender, about, country, city);

            Logger.WriteData(2, "Information", $"FinishRegistration - UserId: {userId}");
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
            Logger.WriteData(2, "Information", $"GetPosts - UserId: {userId}");

            return serializedResponse;
        }

        public static string GetFavouritePosts(int userId, int skipCount)
        {
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangeFavouritePosts(userId, skipCount));
            Logger.WriteData(2, "Information", $"GetFavouritePosts - UserId: {userId}");

            return serializedResponse;
        }

        public static string GetBlogPosts(int userId, int skipCount, int blogId)
        {
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangeBlogPosts(userId, skipCount, blogId));
            Logger.WriteData(2, "Information", $"GetBlogPosts - UserId: {userId}");

            return serializedResponse;
        }

        public static string GetBlogs(int userId)
        {
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId));
            Logger.WriteData(2, "Information", $"GetBlogs - UserId: {userId}");

            return serializedResponse;
        }

        public static string GetBlogs(int userId, string blogName)
        {
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId, blogName));
            Logger.WriteData(2, "Information", $"GetBlogs - UserId: {userId}");

            return serializedResponse;
        }

        public static void Like(int userId, int postId)
        {
            PostLikeLogic.Like(userId, postId);
            Logger.WriteData(2, "Information", $"Like - UserId: {userId}, PostId: {postId}");
        }

        public static void Unlike(int userId, int postId)
        {
            PostLikeLogic.Unlike(userId, postId);
            Logger.WriteData(2, "Information", $"Unlike - UserId: {userId}, PostId: {postId}");

        }

        public static void Favourite(int userId, int postId)
        {
            FavouritePostLogic.Favourite(userId, postId);
            Logger.WriteData(2, "Information", $"Favourite - UserId: {userId}, PostId: {postId}");

        }

        public static void Unfavourite(int userId, int postId)
        {
            FavouritePostLogic.Unfavourite(userId, postId);
            Logger.WriteData(2, "Information", $"Unfavourite - UserId: {userId}, PostId: {postId}");

        }

        public static string BelogsToUser(int userId, int blogId)
        {
            string serializedResponse = BlogLogic.BelogsToUser(userId, blogId).ToString();
            Logger.WriteData(2, "Information", $"BelogsToUser - UserId: {userId}, BlogId: {blogId}");
            return serializedResponse;
        }

        public static void Follow(int userId, int blogId)
        {
            FollowingBlogLogic.Follow(userId, blogId);
            Logger.WriteData(2, "Information", $"Follow - UserId: {userId}, BlogId: {blogId}");

        }

        public static void Unfollow(int userId, int blogId)
        {
            FollowingBlogLogic.Unfollow(userId, blogId);
            Logger.WriteData(2, "Information", $"Unfollow - UserId: {userId}, BlogId: {blogId}");

        }

        public static string GetFollowingBlogs(int userId)
        {
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeFollowedBlogInforamtion(userId));
            Logger.WriteData(2, "Information", $"GetFollowingBlogs - UserId: {userId}");

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
            Logger.WriteData(2, "Information", $"Post - UserId: {userId}, BlogId: {blogId}");

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
            Logger.WriteData(2, "Information", $"GetAnalytics - UserId: {userId}");

            return serializedResponse;
        }

        public static string GetPostsByTag(int userId, int tagId, int skipCount)
        {
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangePostByTag(userId, tagId, skipCount));
            Logger.WriteData(2, "Information", $"GetPostsByTag - UserId: {userId}, TagId: {tagId}");

            return serializedResponse;
        }

        public static void Comment(int userId, int postId, string comment)
        {
            PostCommentLogic.Comment(userId, postId, comment);
            Logger.WriteData(2, "Information", $"Comment - UserId: {userId}, PostId: {postId}, Comment: {comment}");

        }

        public static string GetComments(int userId, int postId)
        {
            string serializedResponse = JsonSerializer.Serialize(PostCommentLogic.GetComments(userId, postId));
            Logger.WriteData(2, "Information", $"GetComments - UserId: {userId}, PostId: {postId}");

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
            Logger.WriteData(2, "Information", $"CreateBlog - UserId: {userId}, BlogName: {blogName}");
        }
    }
}
