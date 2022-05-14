using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
#nullable disable

namespace BusinessLogicLayer
{
    public static class PostBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Post AddPost(Blog blog, Tag[] tags, string content, byte[] image)
        {
            Post newPost = new Post() 
            {
                UserId = UserBusinessLogic.GetCurrentUserId(),
                BlogId = blog.BlogId,
                Content = content,
                Image = image,
                PublishedOn = DateTime.Now
            };
            DbContext.Posts.Add(newPost);

            foreach (Tag tag in tags)
	        {
                PostTagBusinessLogic.AddPostTag(newPost, tag);
	        }

            DbContext.SaveChanges();

            return newPost;
        }

        public static byte[] GetPostImage(int id)
        {
            return DbContext.Posts.Where(post => post.PostId == id).OrderBy(post => post.PostId).First().Image;
        }
        public static string GetPostContent(int id)
        {
            return DbContext.Posts.Where(post => post.PostId == id).OrderBy(post => post.PostId).First().Content;
        }
        public static string GetPostUser(int id)
        {
            return DbContext.Posts.Where(post => post.PostId == id).Include(post => post.User).Select(post => post.User).First().UserName;
        }
    }
}
