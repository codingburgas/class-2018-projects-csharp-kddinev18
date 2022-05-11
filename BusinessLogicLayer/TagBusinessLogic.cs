using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class TagBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Tag AddTag(string tagName)
        {
            Tag newTag = new Tag() 
            {
                TagName = tagName
            };
            DbContext.Tags.Add(newTag);

            DbContext.SaveChanges();

            return newTag;
        }
    }
}
