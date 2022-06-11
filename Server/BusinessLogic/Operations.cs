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
            // Register the user and get the id of the newly registered user
            int userId = UserLogic.Register(userName, email, password);

            // Log the operation
            Logger.WriteData(2, "Information", $"Register - UserId: {userId}, UserName: {userName}");

            // Return the user id
            return userId;
        }

        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            // Add user profile data
            UserProfileLogic.ConfigureUserProfile(userId, gender, about, country, city);

            // Log the operation
            Logger.WriteData(2, "Information", $"FinishRegistration - UserId: {userId}");
        }

        public static string LogIn(string userName, string password)
        {
            // Log in and get sereialisd UserCredentials
            string serializedResponse = JsonSerializer.Serialize(UserLogic.LogIn(userName, password));
            // Log the operation
            Logger.WriteData(2, "Information", "LogIn");

            // Return the serialized data
            return serializedResponse;
        }

        public static int LogInWithCookies(string userName, string password)
        {
            // Log in the user and get the id of the user
            int userId = UserLogic.LogInWithPreHashedPassword(userName, password);
            // Log the operation
            Logger.WriteData(2, "Information", "LogIn");

            // Return the user id
            return userId;
        }

        public static string GetPosts(int userId, int skipCount)
        {
            // Get the serialized post information
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangePosts(userId, skipCount));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetPosts - UserId: {userId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static string GetFavouritePosts(int userId, int skipCount)
        {
            // Get the serialized post information
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangeFavouritePosts(userId, skipCount));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetFavouritePosts - UserId: {userId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static string GetBlogPosts(int userId, int skipCount, int blogId)
        {
            // Get the serialized post information
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangeBlogPosts(userId, skipCount, blogId));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetBlogPosts - UserId: {userId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static string GetBlogs(int userId)
        {
            // Get the serialized blog information
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetBlogs - UserId: {userId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static string GetBlogs(int userId, string blogName)
        {
            // Get the serialized blog information
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId, blogName));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetBlogs - UserId: {userId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static void Like(int userId, int postId)
        {
            // Like post
            PostLikeLogic.Like(userId, postId);
            // Log the operation
            Logger.WriteData(2, "Information", $"Like - UserId: {userId}, PostId: {postId}");
        }

        public static void Unlike(int userId, int postId)
        {
            // Unlike post
            PostLikeLogic.Unlike(userId, postId);
            // Log the operation
            Logger.WriteData(2, "Information", $"Unlike - UserId: {userId}, PostId: {postId}");

        }

        public static void Favourite(int userId, int postId)
        {
            // Favourite post
            FavouritePostLogic.Favourite(userId, postId);
            // Log the operation
            Logger.WriteData(2, "Information", $"Favourite - UserId: {userId}, PostId: {postId}");

        }

        public static void Unfavourite(int userId, int postId)
        {
            // Unfavourite post
            FavouritePostLogic.Unfavourite(userId, postId);
            // Log the operation
            Logger.WriteData(2, "Information", $"Unfavourite - UserId: {userId}, PostId: {postId}");

        }

        public static string BelogsToUser(int userId, int blogId)
        {
            // Get the serialized belongs to user information
            string serializedResponse = BlogLogic.BelogsToUser(userId, blogId).ToString();
            // Log the operation
            Logger.WriteData(2, "Information", $"BelogsToUser - UserId: {userId}, BlogId: {blogId}");
            // Return the serialized data
            return serializedResponse;
        }

        public static void Follow(int userId, int blogId)
        {
            // Follow a blog
            FollowingBlogLogic.Follow(userId, blogId);
            // Log the operation
            Logger.WriteData(2, "Information", $"Follow - UserId: {userId}, BlogId: {blogId}");
        }

        public static void Unfollow(int userId, int blogId)
        {
            // Unollow a blog
            FollowingBlogLogic.Unfollow(userId, blogId);
            // Log the operation
            Logger.WriteData(2, "Information", $"Unfollow - UserId: {userId}, BlogId: {blogId}");
        }

        public static string GetFollowingBlogs(int userId)
        {
            // Get the serialized following blog information
            string serializedResponse = JsonSerializer.Serialize(BlogLogic.ArrangeFollowedBlogInforamtion(userId));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetFollowingBlogs - UserId: {userId}");

            // Return the serialized data
            return serializedResponse;
        }

        public  static void Post(int blogId, int userId, string tags, string content, string image)
        {
            // Conver string to byte array
            string[] stringBytes = image.Split(';');
            byte[] imageBytes = new byte[stringBytes.Length];
            for (int i = 0; i < imageBytes.Length; i++)
            {
                imageBytes[i] = byte.Parse(stringBytes[i]);
            }
            // Add post
            PostLogic.AddPost(blogId, tags.Split(';'), content, imageBytes, userId);
            // Log the operation
            Logger.WriteData(2, "Information", $"Post - UserId: {userId}, BlogId: {blogId}");

        }

        public static string GetTags()
        {
            // Get the serialized tags information
            string serializedResponse = JsonSerializer.Serialize(TagLogic.ArraingeTags());
            // Log the operation
            Logger.WriteData(2, "Information", "GetTags");

            // Return the serialized data
            return serializedResponse;
        }

        public static string GetAnalytics(int userId)
        {
            // Get the serialized analytics information
            string serializedResponse = JsonSerializer.Serialize(UserProfileLogic.ArrangeAnalyticsInformation(userId));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetAnalytics - UserId: {userId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static string GetPostsByTag(int userId, int tagId, int skipCount)
        {
            // Get the serialized post information
            string serializedResponse = JsonSerializer.Serialize(PostLogic.ArrangePostByTag(userId, tagId, skipCount));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetPostsByTag - UserId: {userId}, TagId: {tagId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static void Comment(int userId, int postId, string comment)
        {
            // Comment a post
            PostCommentLogic.Comment(userId, postId, comment);
            // Log the operation
            Logger.WriteData(2, "Information", $"Comment - UserId: {userId}, PostId: {postId}, Comment: {comment}");

        }

        public static string GetComments(int userId, int postId)
        {
            // Get the serialized comments information
            string serializedResponse = JsonSerializer.Serialize(PostCommentLogic.GetComments(userId, postId));
            // Log the operation
            Logger.WriteData(2, "Information", $"GetComments - UserId: {userId}, PostId: {postId}");

            // Return the serialized data
            return serializedResponse;
        }

        public static void CreateBlog(int userId, string image, string blogName)
        {
            // Conver string to byte array
            string[] stringBytes = image.Split(';');
            byte[] imageBytes = new byte[stringBytes.Length];
            for (int i = 0; i < imageBytes.Length; i++)
            {
                imageBytes[i] = byte.Parse(stringBytes[i]);
            }
            // Create post
            BlogLogic.CreateBlog(userId, blogName, imageBytes);
            // Log the operation
            Logger.WriteData(2, "Information", $"CreateBlog - UserId: {userId}, BlogName: {blogName}");
        }
    }
}
