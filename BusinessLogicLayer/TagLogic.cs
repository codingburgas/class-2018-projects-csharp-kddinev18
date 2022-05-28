using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
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
    }
}
