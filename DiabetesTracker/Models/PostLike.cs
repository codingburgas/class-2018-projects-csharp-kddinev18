﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class PostLike
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }

        public static void Like(DiabetesTrackerDbContext dbContext, Post post)
        {
            dbContext.PostLikes.Add(new PostLike() 
            {
                UserId = User.GetCurrentUser(),
                PostId = post.PostId,
            });
            post.LikeCount++;

            dbContext.SaveChanges();
        }
    }
}
