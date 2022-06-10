using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class TagInformation
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int PostCount { get; set; }
    }
    public static class TagLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Tag AddTag(string tagName)
        {
            Tag tag = DbContext.Tags.Where(tag => tag.TagName == tagName).FirstOrDefault();
            if (tag != null) 
                return null;

            Tag newTag = new Tag() 
            {
                TagName = tagName
            };
            DbContext.Tags.Add(newTag);

            return newTag;
        }

        private static List<Tag> GetTags()
        {
            return DbContext.Tags.ToList();
        }

        public static List<TagInformation> ArraingeTags()
        {
            List<Tag> tags = GetTags();

            List<TagInformation> tagsInformation = new List<TagInformation>();
            foreach (Tag tag in tags)
            {
                tagsInformation.Add(new TagInformation()
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName,
                    PostCount = PostTagLogic.GetTagPostCount(tag.TagId)
                });
            }

            return tagsInformation;
        }
    }
}
