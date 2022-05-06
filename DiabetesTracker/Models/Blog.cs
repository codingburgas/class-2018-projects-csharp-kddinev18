using Microsoft.EntityFrameworkCore;
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
        public virtual ICollection<FollowingBlog> FollowingBlogs { get; set; } = new List<FollowingBlog>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

        public static Blog CreateBlog(DiabetesTrackerDbContext dbContext, string name)
        {
            Blog newBlog = new Blog() 
            {
                UserId = User.GetCurrentUser(),
                Name = name,
                CreatedOn = DateTime.Now
            };

            dbContext.Blogs.Add(newBlog);

            dbContext.SaveChanges();

            return newBlog;
        }
        public static ICollection<Post> GetAllPosts(DiabetesTrackerDbContext dbContext, Blog Blog)
        {
            return dbContext.Blogs.Include(blog => blog.Posts).Where(blog => blog.BlogId == Blog.BlogId).Select(blog => blog.Posts).First().ToList();
        }
    }
}
