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
        public static void Comment(Post post, string comment)
        {
            DbContext.PostComments.Add(new PostComment() 
            {
                UserId = UserBusinessLogic.GetCurrentUserId(),
                PostId = post.PostId,
                CommentContend = comment,
                CommentedOn = DateTime.Now,
            });

            DbContext.SaveChanges();
        }
    }
}
