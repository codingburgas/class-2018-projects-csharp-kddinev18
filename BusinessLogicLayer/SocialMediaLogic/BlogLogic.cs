using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    // POCO class
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

        // Adds a Blog into the database
        public static Blog CreateBlog(int userId, string name, byte[] image)
        {
            // Create a new instance of Blog
            Blog newBlog = new Blog() 
            {
                UserId = userId,
                Name = name,
                Image = image,
            };

            // Add the new instance of the Blog into the context
            DbContext.Blogs.Add(newBlog);

            // Save all changes made in this context into the database
            DbContext.SaveChanges();

            // Retuns the newly created blog
            return newBlog;
        }

        // Checks if a blog belongs to a user
        public static bool BelogsToUser(int userId, int blogId)
        {
            return DbContext.Blogs
                // Where the blog id (blog.BlogId) is equal to the given blog id (blogId)
                .Where(blog => blog.BlogId == blogId)
                // Takes the first element
                .First()
                // Checks if the UserId of the currently selected blog is equal to the given userId
                .UserId == userId;
        }

        // Returns all blogs that match a specific name
        public static List<Blog> GetBlogsByName(string blogName)
        {
            return DbContext.Blogs
                // Where the blog name (blog.Name) matches the criteria (blogName)
                .Where(blog=>blog.Name == blogName)
                // Convert the result set to List<>
                .ToList();
        }

        // Returns all blogs belonging to a specific user
        public static List<Blog> GetCurrentUserBlogs(int userId)
        {
            return DbContext.Blogs
                // Where the blog creator (blog.UserId) is equal to the given user id (userId)
                .Where(blog => blog.UserId == userId)
                // Converts the result set to List<>
                .ToList();
        }

        // Returns the number of posts in a blog
        public static int GetBlogPostsCount(int blogId)
        {
            return DbContext.Blogs
                // Includes the navigation property (List<Posts>) from the Post table that are from the specific blog
                .Include(blog => blog.Posts)
                // Where the current blog is equal to the blog we desire
                .Where(blog => blog.BlogId == blogId)
                // Select only the property List<Posts> from the whole result set
                .Select(blog => blog.Posts)
                // Get the first result set
                .First()
                // Gets the count of the List<Posts> property
                .Count();
        }

        // Returns the amount of users following a certain users blogs
        public static int GetBlogFollowingCount(int userId)
        {
            return DbContext.Blogs
                // Where the blog creator (blog.UserId) is equal to the given user (userId)
                .Where(blog => blog.UserId == userId)
                // For every result from the result set sum the followers (blog.FollowingCount)
                .Sum(blog => blog.FollowingCount);
        }

        // Returns the posts in a blog
        public static List<Post> GetAllPosts(int blogId)
        {
            return DbContext.Blogs
                // Includes the navigation property (List<Posts>) from the Post table that are from the specific blog
                .Include(blog => blog.Posts)
                // Where the current blog is equal to the blog we desire
                .Where(blog => blog.BlogId == blogId)
                // Select only the property List<Posts> from the whole result set
                .Select(blog => blog.Posts)
                // Get the first result set
                .First()
                // Convert the result set to List<>
                .ToList();
        }

        // Load blog information from the Blog class to the POCO class BlogInformation
        private static List<BlogInformation> LoadBlogInformation(List<Blog> Blogs, int userId)
        {
            List<BlogInformation> BlogsInformation = new List<BlogInformation>();
            // For every blog in Blogs add a new item to the BlogsInformation list
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
            // Returns the BlogsInformation list
            return BlogsInformation;
        }

        // Recieves the blogs information that belogs to a certain user and then arranges it into the POCO class BlogInformation
        public static List<BlogInformation> ArrangeBlogsInformation(int userId)
        {
            // Recieve blogs information
            List<Blog> currentUserBlogs = GetCurrentUserBlogs(userId);

            // Returns the arranged BlogInformation
            return LoadBlogInformation(currentUserBlogs, userId);
        }

        // Recieves the blogs information that is selected trought the blog name and then arranges it into the POCO class BlogInformation
        public static List<BlogInformation> ArrangeBlogsInformation(int userId, string blogName)
        {
            // Recieve blogs information
            List<Blog> blogsByName = GetBlogsByName(blogName);

            // Returns the arranged BlogInformation
            return LoadBlogInformation(blogsByName, userId);
        }

        // Recieves the blogs information that are followed by a certain user and then arranges it into the POCO class BlogInformation
        public static List<BlogInformation> ArrangeFollowedBlogInforamtion(int userId)
        {
            // Recieve blogs information
            List<Blog> followedBolgs = FollowingBlogLogic.GetFollowingBlogs(userId);

            // Returns the arranged BlogInformation
            return LoadBlogInformation(followedBolgs, userId);
        }

        // Returns the name of the least followed blog
        public static string GetLeastFollowedBlod(int userId)
        {
            Blog blog = DbContext.Blogs
                // Where the blog creator (blog.UserId) is equal to the given user (userId)
                .Where(blog=>blog.UserId == userId)
                // Order the reult set by DESC by the following count
                .OrderByDescending(blog => blog.FollowingCount)
                // Get the first result from  the result set
                .FirstOrDefault();

            // Null check
            if (blog == null)
                return "None";

            return blog.Name;
        }

        // Returns the name of the most followed blog
        public static string GetMostFollowedBlod(int userId)
        {
            Blog blog = DbContext.Blogs
                // Where the blog creator (blog.UserId) is equal to the given user (userId)
                .Where(blog => blog.UserId == userId)
                // Order the reult set by ASC by the following count
                .OrderBy(blog => blog.FollowingCount)
                // Get the first result from  the result set
                .FirstOrDefault();

            // Null check
            if (blog == null)
                return "None";

            return blog.Name;
        }
    }
}
