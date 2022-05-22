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
        public static void Follow(int blogId, int userId)
        {
            DbContext.FollowingBlogs.Add(new FollowingBlog() 
            {
                BlogId = blogId,
                UserId = userId,
            });

            DbContext.SaveChanges();
        }
        public static ICollection<Blog> GetFollowingBlogs(int userId)
        {
            return DbContext.FollowingBlogs.Where(followingBlogs => followingBlogs.UserId == userId).Include(followingBlogs => followingBlogs.Blog).Select(followingBlogs => followingBlogs.Blog).ToList();
        }
    }
}
