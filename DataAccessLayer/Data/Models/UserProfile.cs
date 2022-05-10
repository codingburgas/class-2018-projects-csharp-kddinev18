using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public partial class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public char Gender { get; set; }
        public string About { get; set; } = string.Empty;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public virtual User User { get; set; }

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
