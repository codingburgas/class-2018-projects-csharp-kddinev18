using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public partial class UserProfileBusinessLogic
    {
        public static void ConfigureUserProfile(DiabetesTrackerDbContext dbContext, int userId, char gender, string about, string country, string city)
        {
            dbContext.UserProfiles.Add(new UserProfile()
            {
                UserId = userId,
                Gender = gender,
                About = about,
                DateUpdated = DateTime.Now,
                Country = country,
                City = city
            });

            dbContext.SaveChanges();
        }
    }
}
