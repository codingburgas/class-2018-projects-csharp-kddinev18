using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public static class UserProfileLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static void ConfigureUserProfile(int userId, char gender, string about, string country, string city)
        {
            if(gender == ' ' || about == String.Empty || country == String.Empty || city == String.Empty)
                throw new WrongCredentialsException("You nust fill all of the boxes");

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
