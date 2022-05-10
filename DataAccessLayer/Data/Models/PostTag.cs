using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }

        public static void AddPostTag(DiabetesTrackerDbContext dbContext, Post post, Tag tag)
        {
            dbContext.PostTags.Add(new PostTag() 
            {
                PostId = post.PostId,
                TagId = tag.TagId,
            });
        }
    }
}
