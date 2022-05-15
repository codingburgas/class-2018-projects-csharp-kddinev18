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
        public static List<int> GetBlogsIdByName(string blogName)
        {
            return DbContext.Blogs.Where(blog=>blog.Name==blogName).Select(blod => blod.BlogId).ToList();
        }
        public static List<int> GetCurrentUserBlogs()
        {
            return DbContext.Blogs.Where(blog => blog.UserId == UserBusinessLogic.GetCurrentUserId()).Select(blog => blog.BlogId).ToList();
        }
        public static string GetBlogName(int blogId)
        {
            return DbContext.Blogs.Where(blog => blog.BlogId == blogId).OrderBy(blog => blog.BlogId).First().Name;
        }
        public static int GetBlogPostsCount(int blogId)
        {
            return DbContext.Blogs.Include(blog => blog.Posts).Where(blog => blog.BlogId == blogId).Select(blog => blog.Posts).First().Count();
        }
        public static int GetBlogFollowingCount(int blogId)
        {
            return DbContext.FollowingBlogs.Where(followingBlog => followingBlog.BlogId == blogId).Count();
        }
        public static ICollection<Post> GetAllPosts(int blogId)
        {
            return DbContext.Blogs.Include(blog => blog.Posts).Where(blog => blog.BlogId == blogId).Select(blog => blog.Posts).First().ToList();
        }
    }
}
