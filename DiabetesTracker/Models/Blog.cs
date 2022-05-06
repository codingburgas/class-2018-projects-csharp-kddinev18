using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DiabetesTracker.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public virtual ICollection<FollowingBlog> FollowingBlogs { get; set; } = new List<FollowingBlog>();

        public static Blog CreateBlog(DiabetesTrackerDbContext dbContext, string name)
        {
            Blog blog = new Blog() 
            {
                UserId = User.GetCurrentUser(),
                Name = name,
                CreatedOn = DateTime.Now
            };

            dbContext.Blogs.Add(blog);

            dbContext.SaveChanges();

            return blog;
        }
    }
}
