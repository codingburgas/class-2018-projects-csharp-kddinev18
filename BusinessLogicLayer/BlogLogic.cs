using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
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


        public static List<Tuple<int, byte[], string, int, int>> ArrangeBlogsInformation(int userId)
        {
            List<Blog> currentUserBlogs = GetCurrentUserBlogs(userId);

            List<Tuple<int, byte[], string, int, int>> currentUserBlogsInformation = new List<Tuple<int, byte[], string, int, int>>();
            foreach (Blog currentUserBlog in currentUserBlogs)
            {
                currentUserBlogsInformation.Add(new Tuple<int, byte[], string, int, int>(
                    currentUserBlog.BlogId,
                    currentUserBlog.Image,
                    currentUserBlog.Name,
                    GetBlogPostsCount(currentUserBlog.BlogId),
                    GetBlogFollowingCount(currentUserBlog.BlogId)
                ));
            }
            return currentUserBlogsInformation;
        }
        public static List<Tuple<int, byte[], string, int, int>> ArrangeBlogsInformation(string blogName)
        {
            List<Blog> blogsByName = GetBlogsByName(blogName);

            List<Tuple<int, byte[], string, int, int>> blogsInformationByName = new List<Tuple<int, byte[], string, int, int>>();
            foreach (Blog blogByName in blogsByName)
            {
                blogsInformationByName.Add(new Tuple<int, byte[], string, int, int>(
                    blogByName.BlogId,
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
