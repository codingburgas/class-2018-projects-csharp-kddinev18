using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class PostCommentLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }

        // Add a comment to a specific post
        public static int Comment(int userId, int postId, string comment)
        {
            // Create new instance of a PostComment
            PostComment postComment = new PostComment()
            {
                UserId = userId,
                PostId = postId,
                CommentContend = comment
            };

            // Add the newly created comment into the current context
            DbContext.PostComments.Add(postComment);

            // Save all changes made in this context into the database
            DbContext.SaveChanges();

            // Returns the id of the newly created comment
            return postComment.PostCommentsId;
        }

        // Removes a specific comment
        public static void RemoveComment(int postCommentId)
        {
            DbContext.PostComments
                // Remove a specific comment
                .Remove
                (
                    DbContext.PostComments
                    // Where the post comment matche sthe criteria
                    .Where(postComment => postComment.PostCommentsId == postCommentId)
                    // Take the first result from the result set
                    .First()
                );

            // Save all changes made in this context into the database
            DbContext.SaveChanges();
        }

        // Returns all the comments of a specific post
        public static List<string> GetComments(int userId, int postId)
        {
            return DbContext.PostComments
                // Where the comment creator and the post matcjes the given user and post
                .Where(postComment => postComment.UserId == userId && postComment.PostId == postId)
                // Select only the comment content
                .Select(postComment => postComment.CommentContend)
                // Converts the reult set into a list
                .ToList();
        }
    }
}
