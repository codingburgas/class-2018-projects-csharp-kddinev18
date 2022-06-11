using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class PostLikeLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }

        // Likes a specific post
        public static void Like(int postId, int userId)
        {
            // Create a new instace of a PostLike and add it into the current context
            DbContext.PostLikes.Add(new PostLike() 
            {
                UserId = userId,
                PostId = postId,
            });

            DbContext.Posts
                // Where the post id matches the id of the newly liked post
                .Where(post=>post.PostId == postId)
                // Increase its like count
                .First().LikeCount++;

            // Save all changes made in this context into the database
            DbContext.SaveChanges();
        }

        // Unlikes a specific post
        public static void Unlike(int postId, int userId)
        {
            DbContext.PostLikes
                // Remove a specific PostLike
                .Remove
                (
                    DbContext.PostLikes
                    // Where the post like matches the criteria
                    .Where(postLike => postLike.PostId == postId && postLike.UserId == userId)
                    // Take the first result from the result set
                    .First()
                );

            DbContext.Posts
                // Where the post id matches the id of the newly unliked post
                .Where(post => post.PostId == postId)
                // Dencrease its like count
                .First().LikeCount--;

            // Save all changes made in this context into the database
            DbContext.SaveChanges();
        }

        // Cheks of a post is liked
        public static bool IsCurrentUserLiked(int postId, int userId)
        {
            return DbContext.PostLikes
                // Where the like onwer and the post matches the ciretia we want to check
                .Where(postLike => postLike.PostId == postId && postLike.UserId == userId)
                // Ckeck if the first result from the result set is not null
                .FirstOrDefault() != null;
        }

        // Returns the like count of all of the user's posts
        public static int LikeCount(int userId)
        {
            return DbContext.PostLikes
                // Includes the navigation property Post
                .Include(postlike => postlike.Post)
                // Where the post belogs to a specific user
                .Where(postlike => postlike.Post.UserId == userId)
                // Sum all of the posts like counts
                .Sum(postlike => postlike.Post.LikeCount);
        }
    }
}
