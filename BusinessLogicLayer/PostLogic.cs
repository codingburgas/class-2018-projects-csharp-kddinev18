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

        private static List<Post> GetPagedPosts(int skipCount)
        {
            List<Post> posts = DbContext.Posts.OrderByDescending(post => post.PostId).Skip(skipCount).Take(10).ToList();
            if(posts.Count != 0)
                return posts;

            throw new ArgumentNullException("There are no more posts");
        }

        private static List<Post> GetPagedFavouritePostsIds(int userId, int skipCount)
        {
            List<Post> favouritePostsIds = DbContext.FavouritePosts
                .Where(favouritePost => favouritePost.UserId == userId)
                .OrderByDescending(favouritePost => favouritePost.SavedOn)
                .Include(favouritePost => favouritePost.Post)
                .Select(favouritePost => favouritePost.Post)
                .Skip(skipCount)
                .Take(10)
                .ToList();

            if (favouritePostsIds.Count != 0)
                return favouritePostsIds;

            throw new ArgumentNullException("There are no more posts");
        }

        public static string GetPostBlogName(int postId)
        {
            return DbContext.Posts.Where(post => post.PostId == postId).Include(post => post.Blog).Select(post => post.Blog).First().Name;
        }

        public static List<Tuple<int, string, string, byte[], bool, bool>> ArrangePosts(int userId,int skipCount)
        {
            List<Post> posts = GetPagedPosts(skipCount);

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

        public static List<Tuple<int, string, string, byte[], bool, bool>> ArrangeFavouritePosts(int userId, int skipCount)
        {
            List<Post> posts = GetPagedFavouritePostsIds(userId,skipCount);

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
