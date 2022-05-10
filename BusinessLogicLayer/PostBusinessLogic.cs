using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
#nullable disable

namespace BusinessLogicLayer
{
    public class PostBusinessLogic
    {
        public static Post AddPost(DiabetesTrackerDbContext dbContext, Blog blog, Tag[] tags, string content, byte[] image)
        {
            Post newPost = new Post() 
            {
                UserId = User.GetCurrentUser(),
                BlogId = blog.BlogId,
                Content = content,
                Image = image,
                PublishedOn = DateTime.Now
            };
            dbContext.Posts.Add(newPost);

            foreach (Tag tag in tags)
	        {
                PostTag.AddPostTag(dbContext, newPost, tag);
	        }

            dbContext.SaveChanges();

            return newPost;
        }
    }
}
