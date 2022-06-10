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
        public static int Comment(int userId, int postId, string comment)
        {
            PostComment postComment = new PostComment()
            {
                UserId = userId,
                PostId = postId,
                CommentContend = comment
            };
            DbContext.PostComments.Add(postComment);

            DbContext.SaveChanges();

            return postComment.PostCommentsId;
        }

        public static void RemoveComment(int postCommentId)
        {
            DbContext.PostComments.Remove(DbContext.PostComments.Where(postComment => postComment.PostCommentsId == postCommentId).First());
            DbContext.SaveChanges();
        }

        public static List<string> GetComments(int userId, int postId)
        {
            return DbContext.PostComments.Where(postComment => postComment.UserId == userId && postComment.PostId == postId).Select(postComment => postComment.CommentContend).ToList();
        }
    }
}
