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
    public class PostInformation
    {
        public int PostId { get; set; }
        public string BlogName { get; set; }
        public string PostContent { get; set; }
        public byte[] PostImage { get; set; }
        public bool IsPostLiked { get; set; }
        public bool IsPostFavourited { get; set; }
    }
    public static class PostLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Post AddPost(int blogId, string[] tagNames, string content, byte[] image, int userId)
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
            DbContext.SaveChanges();

            List<Tag> tags = new List<Tag>();

            foreach (string tag in tagNames)
	        {
                tags.Add(TagLogic.AddTag(tag));
	        }
            DbContext.SaveChanges();

            foreach (Tag tag in tags)
            {
                PostTagLogic.AddPostTag(newPost, tag);
            }

            DbContext.SaveChanges();

            return newPost;
        }
        private static List<PostInformation> LoadPostInformation(int userId, List<Post> posts)
        {
            List<PostInformation> postsInformation = new List<PostInformation>();

            foreach (Post post in posts)
            {
                postsInformation.Add(new PostInformation()
                {
                    PostId = post.PostId,
                    BlogName = GetPostBlogName(post.PostId),
                    PostContent = post.Content,
                    PostImage = post.Image,
                    IsPostLiked = PostLikeLogic.IsCurrentUserLiked(post.PostId, userId),
                    IsPostFavourited = FavouritePostLogic.IsCurrentUserFavourited(post.PostId, userId)
                });
            }

            return postsInformation;
        }
        private static List<Post> GetPagedPosts(int skipCount)
        {
            List<Post> posts = DbContext.Posts.OrderByDescending(post => post.PostId).Skip(skipCount).Take(10).ToList();
            if(posts.Count != 0)
                return posts;

            throw new ArgumentNullException("There are no more posts");
        }

        private static List<Post> GetPagedFavouritePosts(int userId, int skipCount)
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

        private static List<Post> GetPagedBlogPosts(int blogId, int skipCount)
        {
            List<Post> posts = DbContext.Posts.Where(post => post.BlogId == blogId).OrderByDescending(post => post.PostId).Skip(skipCount).Take(10).ToList();
            if (posts.Count != 0)
                return posts;

            throw new ArgumentNullException("There are no more posts");
        }
        private static List<Post> GetPagedPostsByTag(int tagId, int skipCount)
        {
            List<Post> posts = DbContext.PostTags.Where(tag => tag.TagId == tagId).Include(tag => tag.Post).Select(tag => tag.Post).Skip(skipCount).Take(10).ToList();
            if (posts.Count != 0)
                return posts;

            throw new ArgumentNullException("There are no more posts");
        }

        public static string GetPostBlogName(int postId)
        {
            return DbContext.Posts.Where(post => post.PostId == postId).Include(post => post.Blog).Select(post => post.Blog).First().Name;
        }

        public static List<PostInformation> ArrangePosts(int userId,int skipCount)
        {
            List<Post> posts = GetPagedPosts(skipCount);

            return LoadPostInformation(userId, posts);
        }

        public static List<PostInformation> ArrangeFavouritePosts(int userId, int skipCount)
        {
            List<Post> posts = GetPagedFavouritePosts(userId,skipCount);

            return LoadPostInformation(userId, posts);
        }

        public static List<PostInformation> ArrangeBlogPosts(int userId, int skipCount, int blogId)
        {
            List<Post> posts = GetPagedBlogPosts(blogId, skipCount);

            return LoadPostInformation(userId, posts);
        }

        public static List<PostInformation> ArrangePostByTag(int userId, int tagId, int skipCount)
        {
            List<Post> posts = GetPagedPostsByTag(tagId, skipCount);

            return LoadPostInformation(userId, posts);
        }

        public static int GetPostCount(int userId)
        {
            return DbContext.Posts.Where(post => post.UserId == userId).Count();
        }
    }
}
