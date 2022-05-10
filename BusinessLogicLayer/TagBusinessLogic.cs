using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class TagBusinessLogic
    {
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
