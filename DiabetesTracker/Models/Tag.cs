using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DiabetesTracker.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        public static Tag AddTag(DiabetesTrackerDbContext dbContext, string tagName)
        {
            Tag newTag = new Tag() 
            {
                TagName = tagName
            };
            dbContext.Tags.Add(newTag);

            dbContext.SaveChanges();

            return newTag;
        }
    }
}
