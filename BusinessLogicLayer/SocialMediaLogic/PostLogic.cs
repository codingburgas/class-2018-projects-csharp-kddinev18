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
    // POCO class
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

        private static int _pagingCount = 10;

        // Creates a post and returns it
        public static Post AddPost(int blogId, string[] tagNames, string content, byte[] image, int userId)
        {
            // Add a new instance of a Post
            Post newPost = new Post() 
            {
                UserId = userId,
                BlogId = blogId,
                Content = content,
                Image = image
            };
            // Add the newly created instace of the post into the current context
            DbContext.Posts.Add(newPost);

            // Save all changes made in this context into the database
            DbContext.SaveChanges();

            List<Tag> tags = new List<Tag>();

            // if the given array tagNames is not null add the tags
            if (tagNames is not null)
            {
                // for every tag name in tagsName add the specific tag
                foreach (string tag in tagNames)
                {
                    tags.Add(TagLogic.AddTag(tag));
                }
                // Save all changes made in this context into the database
                DbContext.SaveChanges();
            }

            // For every tag in the newly added tags add a PostTag
            foreach (Tag tag in tags)
            {
                PostTagLogic.AddPostTag(newPost, tag);
            }

            // Save all changes made in this context into the database
            DbContext.SaveChanges();

            // Returns the newly added post
            return newPost;
        }

        // Returns a list of post information loaded into the PostInformation class
        private static List<PostInformation> LoadPostInformation(int userId, List<Post> posts)
        {
            List<PostInformation> postsInformation = new List<PostInformation>();

            // For every post rearrange the information of this post into the PostInformation class
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

            // Returns the arranged post information
            return postsInformation;
        }

        // Returs a ceratain amount of posts
        private static List<Post> GetPagedPosts(int skipCount)
        {
            List<Post> posts = DbContext.Posts
                // Orders the posts by ther id in DESC
                .OrderByDescending(post => post.PostId)
                // Skip the first n posts
                .Skip(skipCount)
                // Take the next 10 posts
                .Take(_pagingCount)
                // Convert the result set to a list
                .ToList();

            // if the list of posts is not empty return the posts list
            if(posts.Count != 0) return posts;

            // Otherwise thow exception
            throw new ArgumentNullException("There are no more posts");
        }

        // Returs a ceratain amount of favourited posts
        private static List<Post> GetPagedFavouritePosts(int userId, int skipCount)
        {
            List<Post> favouritePostsIds = DbContext.FavouritePosts
                // Where the given user has favourited the post
                .Where(favouritePost => favouritePost.UserId == userId)
                // Order the result set by the date the post have een favourited by DESC
                .OrderByDescending(favouritePost => favouritePost.SavedOn)
                // Include the navigation property Post
                .Include(favouritePost => favouritePost.Post)
                // Select the property Post
                .Select(favouritePost => favouritePost.Post)
                // Skip n amount of posts
                .Skip(skipCount)
                // Take the first 10 posts
                .Take(_pagingCount)
                // Convert the result set to list
                .ToList();

            // if the list of posts is not empty return the posts list
            if (favouritePostsIds.Count != 0) return favouritePostsIds;

            // Otherwise thow exception
            throw new ArgumentNullException("There are no more posts");
        }

        // Returs a ceratain amount of posts belognging to a specific blog
        private static List<Post> GetPagedBlogPosts(int blogId, int skipCount)
        {
            List<Post> posts = DbContext.Posts
                // Where the post belogns to a specific blog
                .Where(post => post.BlogId == blogId)
                // Order the result set by the post id by DESC
                .OrderByDescending(post => post.PostId)
                // Skip n amount of posts
                .Skip(skipCount)
                // Take the first 10 posts
                .Take(_pagingCount)
                // Convert the result set to list
                .ToList();

            // if the list of posts is not empty return the posts list
            if (posts.Count != 0) return posts;

            // Otherwise thow exception
            throw new ArgumentNullException("There are no more posts");
        }

        // Returs a ceratain amount of posts belognging to a specific tag
        private static List<Post> GetPagedPostsByTag(int tagId, int skipCount)
        {
            List<Post> posts = DbContext.PostTags
                // Where the post has the specific tag
                .Where(tag => tag.TagId == tagId)
                // Include the navigation property Post
                .Include(tag => tag.Post)
                // Select the property Post
                .Select(tag => tag.Post)
                // Skip n amount of posts
                .Skip(skipCount)
                // Take the first 10 posts
                .Take(_pagingCount)
                // Convert the result set to list
                .ToList();

            // if the list of posts is not empty return the posts list
            if (posts.Count != 0) return posts;

            // Otherwise thow exception
            throw new ArgumentNullException("There are no more posts");
        }

        // Get a post's blog name
        public static string GetPostBlogName(int postId)
        {
            return DbContext.Posts
                // Where the post matches the specific post id
                .Where(post => post.PostId == postId)
                // Include the navigation property Blog
                .Include(post => post.Blog)
                // Slellect the property Blog
                .Select(post => post.Blog)
                // Get the name of the first blof from the result set
                .First().Name;
        }

        // Returns a list of arranged in PostInformation class post infromation
        public static List<PostInformation> ArrangePosts(int userId,int skipCount)
        {
            // Get the posts
            List<Post> posts = GetPagedPosts(skipCount);

            // Returns the arranged posts
            return LoadPostInformation(userId, posts);
        }

        // Returns a list of arranged in PostInformation class favourited post infromation
        public static List<PostInformation> ArrangeFavouritePosts(int userId, int skipCount)
        {
            // Get the posts
            List<Post> posts = GetPagedFavouritePosts(userId,skipCount);

            // Returns the arranged posts
            return LoadPostInformation(userId, posts);
        }

        // Returns a list of arranged in PostInformation class post that belong in a specific blog infromation
        public static List<PostInformation> ArrangeBlogPosts(int userId, int skipCount, int blogId)
        {
            // Get the posts
            List<Post> posts = GetPagedBlogPosts(blogId, skipCount);

            // Returns the arranged posts
            return LoadPostInformation(userId, posts);
        }

        // Returns a list of arranged in PostInformation class post that has a specific tag infromation
        public static List<PostInformation> ArrangePostByTag(int userId, int tagId, int skipCount)
        {
            // Get the posts
            List<Post> posts = GetPagedPostsByTag(tagId, skipCount);

            // Returns the arranged posts
            return LoadPostInformation(userId, posts);
        }

        // Returns the count of a user's posts
        public static int GetPostCount(int userId)
        {
            return DbContext.Posts
                // Where the post onwer mathes the specific user
                .Where(post => post.UserId == userId)
                // Gets the count of the result set
                .Count();
        }
    }
}
