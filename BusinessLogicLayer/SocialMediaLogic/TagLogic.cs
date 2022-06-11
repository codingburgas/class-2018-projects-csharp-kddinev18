using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    // POCO class
    public class TagInformation
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int PostCount { get; set; }
    }
    public static class TagLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }

        // Creates a new tag and returns it
        public static Tag AddTag(string tagName)
        {
            Tag tag = DbContext.Tags
                // Where the tag name is the same as the specified tag name
                .Where(tag => tag.TagName == tagName)
                // Take te first result from the result set
                .FirstOrDefault();

            // Check is the tag is existing
            if (tag != null) 
                // Return null if the tag exists
                return null;

            // Create new instance of a tag
            Tag newTag = new Tag() 
            {
                TagName = tagName
            };

            // Add the newly created tag into the curent context
            DbContext.Tags.Add(newTag);

            // Return the new tag
            return newTag;
        }

        // Returns a list of all tags
        private static List<Tag> GetTags()
        {
            return DbContext.Tags.ToList();
        }

        // Returns a list of arranged tag information
        public static List<TagInformation> ArraingeTags()
        {
            // Get all the tags
            List<Tag> tags = GetTags();

            List<TagInformation> tagsInformation = new List<TagInformation>();

            // Rearrange the tag information into the TagInformation class
            foreach (Tag tag in tags)
            {
                tagsInformation.Add(new TagInformation()
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName,
                    PostCount = PostTagLogic.GetTagPostCount(tag.TagId)
                });
            }

            // Returns the arranged tags information
            return tagsInformation;
        }
    }
}
