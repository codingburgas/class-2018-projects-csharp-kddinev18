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
        public static Blog CreateBlog(string name, byte[] image)
        {
            Blog newBlog = new Blog() 
            {
                UserId = UserBusinessLogic.GetCurrentUserId(),
                Name = name,
                Image = image,
                CreatedOn = DateTime.Now
            };

            DbContext.Blogs.Add(newBlog);

            DbContext.SaveChanges();

            return newBlog;
        }
        public static List<Blog> GetBlogsByName(string blogName)
        {
            return DbContext.Blogs.Where(blog=>blog.Name==blogName).ToList();
        }
        public static List<Blog> GetCurrentUserBlogs()
        {
            return DbContext.Blogs.Where(blog => blog.UserId == UserBusinessLogic.GetCurrentUserId()).ToList();
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


        public static List<Tuple<byte[], string, int, int>> GetCurrentUserBlogsInformation()
        {
            List<Blog> currentUserBlogs = GetCurrentUserBlogs();

            List<Tuple<byte[], string, int, int>> currentUserBlogsInformation = new List<Tuple<byte[], string, int, int>>();
            foreach (Blog currentUserBlog in currentUserBlogs)
            {
                currentUserBlogsInformation.Add(new Tuple<byte[], string, int, int>(
                    currentUserBlog.Image,
                    currentUserBlog.Name,
                    GetBlogPostsCount(currentUserBlog.BlogId),
                    GetBlogFollowingCount(currentUserBlog.BlogId)
                ));
            }
            return currentUserBlogsInformation;
        }
        public static List<Tuple<byte[], string, int, int>> GetBlogsInformationByName(string blogName)
        {
            List<Blog> blogsByName = GetBlogsByName(blogName);

            List<Tuple<byte[], string, int, int>> blogsInformationByName = new List<Tuple<byte[], string, int, int>>();
            foreach (Blog blogByName in blogsByName)
            {
                blogsInformationByName.Add(new Tuple<byte[], string, int, int>(
                    blogByName.Image,
                    blogByName.Name,
                    GetBlogPostsCount(blogByName.BlogId),
                    GetBlogFollowingCount(blogByName.BlogId)
                ));
            }
            return blogsInformationByName;
        }
    }
}
