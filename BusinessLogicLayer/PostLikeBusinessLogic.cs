using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class PostLikeBusinessLogic
    {
        public static void Like(DiabetesTrackerDbContext dbContext, Post post)
        {
            dbContext.PostLikes.Add(new PostLike() 
            {
                UserId = UserBusinessLogic.GetCurrentUser(),
                PostId = post.PostId,
            });
            post.LikeCount++;

            dbContext.SaveChanges();
        }

        public static int UpdateLikeCount(DiabetesTrackerDbContext dbContext, Post post)
        {
            int likesCount = dbContext.PostLikes.Where(postLike => postLike.PostId == post.PostId).Count();
            post.LikeCount = likesCount;
            return likesCount;
        }
    }
}
