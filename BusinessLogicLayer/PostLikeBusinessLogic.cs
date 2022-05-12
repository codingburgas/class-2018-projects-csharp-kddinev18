using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class PostLikeBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static void Like(Post post)
        {
            DbContext.PostLikes.Add(new PostLike() 
            {
                UserId = UserBusinessLogic.GetCurrentUserId(),
                PostId = post.PostId,
            });
            post.LikeCount++;

            DbContext.SaveChanges();
        }

        public static int UpdateLikeCount(Post post)
        {
            int likesCount = DbContext.PostLikes.Where(postLike => postLike.PostId == post.PostId).Count();
            post.LikeCount = likesCount;
            return likesCount;
        }
    }
}
