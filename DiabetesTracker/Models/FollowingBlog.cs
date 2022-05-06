using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class FollowingBlog
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public DateTime FollowedOn { get; set; }

        public virtual User User { get; set; }
        public virtual Blog Blog { get; set; }

        public static void Follow(DiabetesTrackerDbContext dbContext, Blog blog)
        {
            dbContext.FollowingBlogs.Add(new FollowingBlog() 
            {
                BlogId = blog.BlogId,
                UserId = User.GetCurrentUser(),
            });

            dbContext.SaveChanges();
        }
    }
}
