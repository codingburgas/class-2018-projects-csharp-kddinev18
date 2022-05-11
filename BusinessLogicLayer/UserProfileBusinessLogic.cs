using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public static class UserProfileBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static void ConfigureUserProfile(int userId, char gender, string about, string country, string city)
        {
            DbContext.UserProfiles.Add(new UserProfile()
            {
                UserId = userId,
                Gender = gender,
                About = about,
                DateUpdated = DateTime.Now,
                Country = country,
                City = city
            });

            DbContext.SaveChanges();
        }
    }
}
