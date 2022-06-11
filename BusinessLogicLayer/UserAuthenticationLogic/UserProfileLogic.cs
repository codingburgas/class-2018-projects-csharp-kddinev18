using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    // POCO class
    public class AnalyticsInformation
    {
        public int PostCount { get; set; }
        public int Likes { get; set; }
        public int Followers { get; set; }
        public int BlogCount { get; set; }
        public string MostFollowedBlog { get; set; }
        public string LeastFollowedBlog { get; set; }
    }
    public static class UserProfileLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        
        // Add user profile data
        public static void ConfigureUserProfile(int userId, char gender, string about, string country, string city)
        {
            // If some of the arguments are null throw an excpetion
            if(gender == ' ' || about == String.Empty || country == String.Empty || city == String.Empty)
                throw new ArgumentNullException("You nust fill all of the boxes");

            // Add new instace of a UserProfile into the current context
            DbContext.UserProfiles.Add(new UserProfile()
            {
                UserId = userId,
                Gender = gender,
                About = about,
                DateUpdated = DateTime.Now,
                Country = country,
                City = city
            });

            // Add the newly added user into the current context
            DbContext.SaveChanges();
        }

        // Arranges the anlytics information into AnalyticsInformation
        public static AnalyticsInformation ArrangeAnalyticsInformation(int userId)
        {
            return new AnalyticsInformation() {
                PostCount = PostLogic.GetPostCount(userId),
                Likes = PostLikeLogic.LikeCount(userId),
                Followers = BlogLogic.GetBlogFollowingCount(userId),
                BlogCount = BlogLogic.GetCurrentUserBlogs(userId).Count,
                MostFollowedBlog = BlogLogic.GetMostFollowedBlod(userId),
                LeastFollowedBlog = BlogLogic.GetLeastFollowedBlod(userId)
            };
        }
    }
}
