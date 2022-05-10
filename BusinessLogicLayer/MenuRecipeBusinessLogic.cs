using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public class MenuRecipeBusinessLogic
    {
        public static void AddMenuRecipes(DiabetesTrackerDbContext dbContext, Menu menu, Recipe recipe)
        {
            dbContext.MenuRecipes.Add(new MenuRecipe()
            {
                MenuId = menu.MenuId,
                RecipeId = recipe.RecipeId,
            });
        }
    }
}
