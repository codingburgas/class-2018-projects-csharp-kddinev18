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
    public static class PostLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Post AddPost(int blogId, Tag[] tags, string content, byte[] image, int userId)
        {
            Post newPost = new Post() 
            {
                UserId = userId,
                BlogId = blogId,
                Content = content,
                Image = image,
                PublishedOn = DateTime.Now
            };
            DbContext.Posts.Add(newPost);

            foreach (Tag tag in tags)
	        {
                PostTagLogic.AddPostTag(newPost, tag);
	        }

            DbContext.SaveChanges();

            return newPost;
        }

        private static List<Post> GetPost(int skipCount)
        {
            List<Post> posts = DbContext.Posts.OrderByDescending(post => post.PostId).Skip(skipCount).Take(10).ToList();
            if(posts.Count != 0)
                return posts;

            throw new ArgumentNullException("No more posts");
        }
        public static string GetPostBlogName(int postId)
        {
            return DbContext.Posts.Where(post => post.PostId == postId).Include(post => post.Blog).Select(post => post.Blog).First().Name;
        }

        public static List<Tuple<int, string, string, byte[],bool,bool>> GetPosts(int userId,int skipCount)
        {
            List<Post> posts = GetPost(skipCount);

            List<Tuple<int, string, string, byte[], bool, bool>> postsInformation = new List<Tuple<int, string, string, byte[], bool, bool>>();

            foreach (Post post in posts)
            {
                postsInformation.Add(new Tuple<int, string, string, byte[], bool, bool>(
                    post.PostId,
                    GetPostBlogName(post.PostId),
                    post.Content,
                    post.Image,
                    PostLikeLogic.IsCurrentUserLiked(post.PostId, userId),
                    FavouritePostLogic.IsCurrentUserFavourited(post.PostId, userId)
                ));
            }

            return postsInformation;
        }
    }
}
