using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public class PostCommentBusinessLogic
    {
        public static void Comment(DiabetesTrackerDbContext dbContext, Post post, string comment)
        {
            dbContext.PostComments.Add(new PostComment() 
            {
                UserId = User.GetCurrentUser(),
                PostId = post.PostId,
                CommentContend = comment,
                CommentedOn = DateTime.Now,
            });

            dbContext.SaveChanges();
        }
    }
}
