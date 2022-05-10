using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class BlogBusinessLogic
    {
        public static Blog CreateBlog(DiabetesTrackerDbContext dbContext, string name)
        {
            Blog newBlog = new Blog() 
            {
                UserId = UserBusinessLogic.GetCurrentUser(),
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
