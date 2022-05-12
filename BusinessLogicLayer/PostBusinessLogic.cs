using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public static Image ConvertBytesToImage()
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                return Image.FromStream(memoryStream);
            }
        }

        public static byte[] ConvertImageToBytes(Image image)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bf.Serialize(memoryStream, new ImageConverter().ConvertTo(image, typeof(byte[])));
                return memoryStream.ToArray();
            }
        }
    }
}
