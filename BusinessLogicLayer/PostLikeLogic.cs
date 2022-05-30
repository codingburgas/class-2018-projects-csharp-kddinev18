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
        public static void Like(int postId, int userId)
        {
            DbContext.PostLikes.Add(new PostLike() 
            {
                UserId = userId,
                PostId = postId,
            });
            DbContext.Posts.Where(post=>post.PostId == postId).First().LikeCount++;

            DbContext.SaveChanges();
        }
        public static void Unlike(int postId, int userId)
        {
            DbContext.PostLikes.Remove(DbContext.PostLikes.Where(postLike => postLike.PostId == postId && postLike.UserId == userId).First());
            DbContext.Posts.Where(post => post.PostId == postId).First().LikeCount--;

            DbContext.SaveChanges();
        }
        public static bool IsCurrentUserLiked(int postId, int userId)
        {
            return DbContext.PostLikes.Where(postLike => postLike.PostId == postId && postLike.UserId == userId).FirstOrDefault() != null;
        }
        public static int LikeCount(int userId)
        {
            return DbContext.PostLikes.Include(postlike => postlike.Post).Where(postlike => postlike.Post.UserId == userId).Sum(postlike => postlike.Post.LikeCount);
        }
    }
}
