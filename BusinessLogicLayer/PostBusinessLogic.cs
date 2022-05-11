using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                UserId = User.GetCurrentUser(),
                BlogId = blog.BlogId,
                Content = content,
                Image = image,
                PublishedOn = DateTime.Now
            };
            DbContext.Posts.Add(newPost);

            foreach (Tag tag in tags)
	        {
                PostTag.AddPostTag(DbContext, newPost, tag);
	        }

            DbContext.SaveChanges();

            return newPost;
        }
    }
}
