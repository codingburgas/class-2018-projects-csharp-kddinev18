using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class FollowingBlogBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static void Follow(Blog blog)
        {
            DbContext.FollowingBlogs.Add(new FollowingBlog() 
            {
                BlogId = blog.BlogId,
                UserId = User.GetCurrentUser(),
            });

            DbContext.SaveChanges();
        }
        public static ICollection<Blog> GetFollowingBlogs()
        {
            return DbContext.FollowingBlogs.Where(followingBlogs => followingBlogs.UserId == UserBusinessLogic.GetCurrentUserId()).Include(followingBlogs => followingBlogs.Blog).Select(followingBlogs => followingBlogs.Blog).ToList();
        }
    }
}
