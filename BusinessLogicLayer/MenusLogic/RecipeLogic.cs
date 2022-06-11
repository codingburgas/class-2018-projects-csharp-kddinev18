using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class RecipeLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }

        // Adds Recipe into the daatabse
        public static Recipe CreateRecipe(int userId, string name, string content)
        {
            // Create new instance of a Recipe
            Recipe newRecipe = new Recipe() 
            {
                UserId = userId,
                Name = name,
                Content = content,
                CreatedOn = DateTime.Now,
            };
            // Adds the new instance of the Recipe into the context
            DbContext.Recipes.Add(newRecipe);

            // Save all changes made in this context into the database
            DbContext.SaveChanges();

            return newRecipe;
        }
    }
}
