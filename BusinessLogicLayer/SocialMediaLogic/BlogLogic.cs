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
        public bool IsFollowed { get; set; }
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
        public static int GetBlogFollowingCount(int userId)
        {
            return DbContext.Blogs.Where(blog => blog.UserId == userId).Sum(blog => blog.FollowingCount);
        }
        public static List<Post> GetAllPosts(int blogId)
        {
            return DbContext.Blogs.Include(blog => blog.Posts).Where(blog => blog.BlogId == blogId).Select(blog => blog.Posts).First().ToList();
        }

        private static List<BlogInformation> LoadBlogInformation(List<Blog> Blogs, int userId)
        {
            List<BlogInformation> BlogsInformation = new List<BlogInformation>();
            foreach (Blog Blog in Blogs)
            {
                BlogsInformation.Add(new BlogInformation()
                {
                    BlogId = Blog.BlogId,
                    BlogImage = Blog.Image,
                    BlogName = Blog.Name,
                    PostCount = GetBlogPostsCount(Blog.BlogId),
                    FollowingCount = Blog.FollowingCount,
                    IsFollowed = FollowingBlogLogic.IsCurrentUserFollowed(Blog.BlogId, userId)
                });
            }
            return BlogsInformation;
        }

        public static List<BlogInformation> ArrangeBlogsInformation(int userId)
        {
            List<Blog> currentUserBlogs = GetCurrentUserBlogs(userId);

            return LoadBlogInformation(currentUserBlogs, userId);
        }

        public static List<BlogInformation> ArrangeBlogsInformation(int userId, string blogName)
        {
            List<Blog> blogsByName = GetBlogsByName(blogName);

            return LoadBlogInformation(blogsByName, userId);
        }

        public static List<BlogInformation> ArrangeFollowedBlogInforamtion(int userId)
        {
            List<Blog> followedBolgs = FollowingBlogLogic.GetFollowingBlogs(userId);

            return LoadBlogInformation(followedBolgs, userId);
        }

        public static string GetLeastFollowedBlod(int userId)
        {
            Blog blog = DbContext.Blogs.Where(blog=>blog.UserId == userId).OrderByDescending(blog => blog.FollowingCount).FirstOrDefault();
            if (blog == null)
                return "None";

            return blog.Name;
        }

        public static string GetMostFollowedBlod(int userId)
        {
            Blog blog = DbContext.Blogs.Where(blog => blog.UserId == userId).OrderBy(blog => blog.FollowingCount).FirstOrDefault();
            if (blog == null)
                return "None";

            return blog.Name;
        }
    }
}
