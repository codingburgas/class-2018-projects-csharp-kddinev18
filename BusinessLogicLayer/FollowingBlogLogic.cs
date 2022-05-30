using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class FollowingBlogLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static void Follow(int userId, int blogId)
        {
            DbContext.FollowingBlogs.Add(new FollowingBlog() 
            {
                BlogId = blogId,
                UserId = userId,
            });

            DbContext.Blogs.Where(blog => blog.BlogId == blogId).FirstOrDefault().FollowingCount++;

            DbContext.SaveChanges();
        }
        public static void Unfollow(int blogId, int userId)
        {
            DbContext.FollowingBlogs.Remove(DbContext.FollowingBlogs.Where(followingBlog => followingBlog.BlogId == blogId && followingBlog.UserId == userId).First());

            DbContext.Blogs.Where(blog => blog.BlogId == blogId).FirstOrDefault().FollowingCount--;

            DbContext.SaveChanges();
        }
        public static bool IsCurrentUserFollowed(int userId, int blogId)
        {
            return DbContext.FollowingBlogs.Where(followingBlog => followingBlog.UserId == userId && followingBlog.BlogId == blogId).ToList().Count == 1;
        }
        public static List<Blog> GetFollowingBlogs(int userId)
        {
            return DbContext.FollowingBlogs.Where(followingBlogs => followingBlogs.UserId == userId).Include(followingBlogs => followingBlogs.Blog).Select(followingBlogs => followingBlogs.Blog).ToList();
        }
    }
}
