using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class PostTagLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static PostTag AddPostTag(Post post, Tag tag)
        {
            if (post == null || tag == null)
                return null;

            PostTag postTag = new PostTag()
            {
                PostId = post.PostId,
                TagId = tag.TagId,
            };

            DbContext.PostTags.Add(postTag);

            return postTag;
        }
        public static int GetTagPostCount(int tagId)
        {
            return DbContext.PostTags.Where(postTag => postTag.TagId == tagId).Count();
        }
    }
}
