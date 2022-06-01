using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

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
                CommentContend = comment,
                CommentedOn = DateTime.Now,
            });

            DbContext.SaveChanges();
        }
    }
}
