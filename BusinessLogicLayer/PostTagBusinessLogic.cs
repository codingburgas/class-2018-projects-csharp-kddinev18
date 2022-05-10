using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public class PostTagBusinessLogic
    {
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
