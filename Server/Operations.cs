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
            return UserLogic.Register(userName, email, password);
        }

        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            UserProfileLogic.ConfigureUserProfile(userId, gender, about, country, city);
        }

        public static string LogIn(string userName, string password)
        {
            return JsonSerializer.Serialize(UserLogic.LogIn(userName, password));
        }

        public static int LogInWithCookies(string userName, string password)
        {
            return UserLogic.LogInWithPreHashedPassword(userName, password);
        }

        public static string GetPosts(int userId, int skipCount)
        {
            return JsonSerializer.Serialize(PostLogic.ArrangePosts(userId, skipCount));
        }

        public static string GetFavouritePosts(int userId, int skipCount)
        {
            return JsonSerializer.Serialize(PostLogic.ArrangeFavouritePosts(userId, skipCount));
        }

        public static string GetBlogPosts(int userId, int skipCount, int blogId)
        {
            return JsonSerializer.Serialize(PostLogic.ArrangeBlogPosts(userId, skipCount, blogId));
        }

        public static string GetBlogs(int userId)
        {
            return JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId));
        }

        public static string GetBlogs(int userId, string blogName)
        {
            return JsonSerializer.Serialize(BlogLogic.ArrangeBlogsInformation(userId, blogName));
        }

        public static void Like(int userId, int postId)
        {
            PostLikeLogic.Like(userId, postId);
        }

        public static void Unlike(int userId, int postId)
        {
            PostLikeLogic.Unlike(userId, postId);
        }

        public static void Favourite(int userId, int postId)
        {
            FavouritePostLogic.Favourite(userId, postId);
        }

        public static void Unfavourite(int userId, int postId)
        {
            FavouritePostLogic.Unfavourite(userId, postId);
        }

        public static string BelogsToUSer(int userId, int postId)
        {
            return BlogLogic.BelogsToUser(userId, postId).ToString();
        }

        public static void Follow(int userId, int blogId)
        {
            FollowingBlogLogic.Follow(userId, blogId);
        }

        public static void Unfollow(int userId, int blogId)
        {
            FollowingBlogLogic.Unfollow(userId, blogId);
        }

        public static string GetFollowingBlogs(int userId)
        {
            return JsonSerializer.Serialize(BlogLogic.ArrangeFollowedBlogInforamtion(userId));
        }

        public  static void Post(int blogId, int userId, string tags, string content, string image)
        {
            string[] imageBytes = image.Split(';');
            byte[] bytes = new byte[imageBytes.Length];
            for (int i = 0; i < imageBytes.Length; i++)
            {
                bytes[i] = byte.Parse(imageBytes[i]);
            }
            PostLogic.AddPost(blogId, tags.Split(';'), content, bytes, userId);
        }

        public static string GetTags()
        {
            return JsonSerializer.Serialize(TagLogic.ArraingeTags());
        }

        public static string GetAnalytics(int userId)
        {
            return JsonSerializer.Serialize(UserProfileLogic.ArrangeAnalyticsInformation(userId));
        }

        public static string GetPostsByTag(int userId, int tagId, int skipCount)
        {
            return JsonSerializer.Serialize(PostLogic.ArrangePostByTag(userId, tagId, skipCount));
        }
    }
}
