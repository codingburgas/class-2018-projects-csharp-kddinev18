using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class FollowingBlogBusinessLogic
    {
        public static void Follow(DiabetesTrackerDbContext dbContext, Blog blog)
        {
            dbContext.FollowingBlogs.Add(new FollowingBlog() 
            {
                BlogId = blog.BlogId,
                UserId = User.GetCurrentUser(),
            });

            dbContext.SaveChanges();
        }
        public static ICollection<Blog> GetFollowingBlogs(DiabetesTrackerDbContext dbContext)
        {
            return dbContext.FollowingBlogs.Where(followingBlogs => followingBlogs.UserId == UserBusinessLogic.GetCurrentUser()).Include(followingBlogs => followingBlogs.Blog).Select(followingBlogs => followingBlogs.Blog).ToList();
        }
    }
}
