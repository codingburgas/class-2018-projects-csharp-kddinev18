using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public static List<Post> GetPost(int skipCount)
        {
            List<Post> posts = DbContext.Posts.OrderByDescending(post => post.PostId).Skip(skipCount).Take(10).ToList();
            if(posts.Count != 0)
                return posts;

            throw new ArgumentNullException("No more posts");
        }
        public static byte[] GetPostImage(int postId)
        {
            return DbContext.Posts.Where(post => post.PostId == postId).OrderBy(post => post.PostId).First().Image;
        }
        public static string GetPostContent(int postId)
        {
            return DbContext.Posts.Where(post => post.PostId == postId).OrderBy(post => post.PostId).First().Content;
        }
        public static string GetPostBlogName(int postId)
        {
            return DbContext.Posts.Where(post => post.PostId == postId).Include(post => post.Blog).Select(post => post.Blog).First().Name;
        }
        public static List<Tuple<string, string, byte[]>> GetPosts(int skipCount)
        {
            List<Post> posts = GetPost(skipCount);

            List<Tuple<string, string, byte[]>> postsInformation = new List<Tuple<string, string, byte[]>>();

            foreach (Post post in posts)
            {
                postsInformation.Add(new Tuple<string, string, byte[]>(
                    GetPostBlogName(post.PostId),
                    post.Content,
                    post.Image
                ));
            }

            return postsInformation;
        }
    }
}
