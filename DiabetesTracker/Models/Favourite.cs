using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DiabetesTracker.Models
{
    public class FavouritePost
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime SavedOn { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }

        public static void Favourite(DiabetesTrackerDbContext dbContext, Post post)
        {
            dbContext.FavouritePosts.Add(new FavouritePost()
            {
                UserId = User.GetCurrentUser(),
                PostId = post.PostId,
                SavedOn = DateTime.Now,
            });
        }
    }
}
