using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class BlogInformation
    {
        public int BlogId { get; set; }
        public byte[] BlogImage { get; set; }
        public string BlogName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
    }
    public static class BlogLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Blog CreateBlog(int userId, string name, byte[] image)
        {
            Blog newBlog = new Blog() 
            {
                UserId = userId,
                Name = name,
                Image = image,
                CreatedOn = DateTime.Now
            };

            DbContext.Blogs.Add(newBlog);

            DbContext.SaveChanges();

            return newBlog;
        }
        public static bool BelogsToUser(int userId, int blogId)
        {
            return DbContext.Blogs.Where(blog => blog.BlogId == blogId).First().UserId == userId;
        }
        public static List<Blog> GetBlogsByName(string blogName)
        {
            return DbContext.Blogs.Where(blog=>blog.Name==blogName).ToList();
        }
        public static List<Blog> GetCurrentUserBlogs(int userId)
        {
            return DbContext.Blogs.Where(blog => blog.UserId == userId).ToList();
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

        private static List<BlogInformation> LoadBlogInformation(List<Blog> currentUserBlogs)
        {
            List<BlogInformation> currentUserBlogsInformation = new List<BlogInformation>();
            foreach (Blog currentUserBlog in currentUserBlogs)
            {
                currentUserBlogsInformation.Add(new BlogInformation()
                {
                    BlogId = currentUserBlog.BlogId,
                    BlogImage = currentUserBlog.Image,
                    BlogName = currentUserBlog.Name,
                    PostCount = GetBlogPostsCount(currentUserBlog.BlogId),
                    FollowingCount = GetBlogFollowingCount(currentUserBlog.BlogId)
                });
            }
            return currentUserBlogsInformation;
        }

        public static List<BlogInformation> ArrangeBlogsInformation(int userId)
        {
            List<Blog> currentUserBlogs = GetCurrentUserBlogs(userId);

            return LoadBlogInformation(currentUserBlogs);
        }

        public static List<BlogInformation> ArrangeBlogsInformation(string blogName)
        {
            List<Blog> blogsByName = GetBlogsByName(blogName);

            return LoadBlogInformation(blogsByName);
        }
    }
}
