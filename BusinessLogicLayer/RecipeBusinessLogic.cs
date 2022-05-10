using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public class RecipeBusinessLogic
    {
        public static Recipe CreateRecipe(DiabetesTrackerDbContext dbContext, string name, string content)
        {
            Recipe newRecipe = new Recipe() 
            {
                UserId = UserBusinessLogic.GetCurrentUser(),
                Name = name,
                Content = content,
                CreatedOn = DateTime.Now,
            };
            dbContext.Recipes.Add(newRecipe);

            dbContext.SaveChanges();

            return newRecipe;
        }
    }
}
