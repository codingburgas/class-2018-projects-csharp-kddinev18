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

        // Creates a Post tag
        public static PostTag AddPostTag(Post post, Tag tag)
        {
            // if the post or the tag is null return null
            if (post == null || tag == null) return null;

            // Create new instance of a PostTag
            PostTag postTag = new PostTag()
            {
                PostId = post.PostId,
                TagId = tag.TagId,
            };

            // Add the newly created PostTag into the current context
            DbContext.PostTags.Add(postTag);

            // Returns the newly created PostTag
            return postTag;
        }

        // Returnts the count of a tag's posts
        public static int GetTagPostCount(int tagId)
        {
            return DbContext.PostTags
                // Where the tag matcjes the given tag id
                .Where(postTag => postTag.TagId == tagId)
                // Get the count of the result set
                .Count();
        }
    }
}
