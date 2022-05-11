using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class BlogBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Blog CreateBlog(string name)
        {
            Blog newBlog = new Blog() 
            {
                UserId = UserBusinessLogic.GetCurrentUserId(),
                Name = name,
                CreatedOn = DateTime.Now
            };

            DbContext.Blogs.Add(newBlog);

            DbContext.SaveChanges();

            return newBlog;
        }
        public static ICollection<Post> GetAllPosts(Blog Blog)
        {
            return DbContext.Blogs.Include(blog => blog.Posts).Where(blog => blog.BlogId == Blog.BlogId).Select(blog => blog.Posts).First().ToList();
        }
    }
}
