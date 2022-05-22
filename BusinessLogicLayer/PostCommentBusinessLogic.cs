using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public static class PostCommentBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static void Comment(int postId, int userId, string comment)
        {
            DbContext.PostComments.Add(new PostComment() 
            {
                UserId = userId,
                PostId = postId,
                CommentContend = comment,
                CommentedOn = DateTime.Now,
            });

            DbContext.SaveChanges();
        }
    }
}
