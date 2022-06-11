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

        // Follows a specific blog
        public static void Follow(int userId, int blogId)
        {
            // Add new instance of a FollowingBlog into the current context
            DbContext.FollowingBlogs.Add(new FollowingBlog() 
            {
                BlogId = blogId,
                UserId = userId,
            });

            DbContext.Blogs
                // Where the blog's id matches the just followed blog id
                .Where(blog => blog.BlogId == blogId)
                // Take the first result from the result set and increase its following count
                .First().FollowingCount++;


            // Save all changes made in this context into the database
            DbContext.SaveChanges();
        }

        // Unfollows a specific blog
        public static void Unfollow(int userId, int blogId)
        {
            DbContext.FollowingBlogs
                // Remove the specified FollowingBlog
                .Remove
                (
                    DbContext.FollowingBlogs
                    // Where the given blog is followed by the given user
                    .Where(followingBlog => followingBlog.BlogId == blogId && followingBlog.UserId == userId)
                    // Take the first result from the result set
                    .First()
                );

            DbContext.Blogs
                // Where the blog's id matches the just unfollowed blog id
                .Where(blog => blog.BlogId == blogId)
                // Take the first result from the result set and decrease its following count
                .First().FollowingCount--;

            // Save all changes made in this context into the database
            DbContext.SaveChanges();
        }

        // Check if the current blog is followed by a guven user
        public static bool IsCurrentUserFollowed(int userId, int blogId)
        {
            return DbContext.FollowingBlogs
                // Where the given blog is followed by the given user
                .Where(followingBlog => followingBlog.UserId == userId && followingBlog.BlogId == blogId)
                // Convert the result set to list and checks if its count is equal to 1
                .ToList().Count == 1;
        }

        // Resturns a list of followed blogs
        public static List<Blog> GetFollowingBlogs(int userId)
        {
            return DbContext.FollowingBlogs
                // Where the given user has followed the blog
                .Where(followingBlogs => followingBlogs.UserId == userId)
                // Include the navigation property Blog
                .Include(followingBlogs => followingBlogs.Blog)
                // Select the property Blog from the result set
                .Select(followingBlogs => followingBlogs.Blog)
                // Converts the result set into a list
                .ToList();
        }
    }
}
