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
        public static void Comment(int userId, int postId, string comment)
        {
            DbContext.PostComments.Add(new PostComment() 
            {
                UserId = userId,
                PostId = postId,
                CommentContend = comment
            });

            DbContext.SaveChanges();
        }
        public static List<string> GetComments(int userId, int postId)
        {
            return DbContext.PostComments.Where(postComment => postComment.UserId == userId && postComment.PostId == postId).Select(postComment => postComment.CommentContend).ToList();
        }
    }
}
