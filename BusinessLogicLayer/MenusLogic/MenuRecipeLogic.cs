using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public class MenuRecipeLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }

        // Adds MenuRecipe to the databse
        public static void AddMenuRecipes(Menu menu, Recipe recipe)
        {
            DbContext.MenuRecipes.Add(new MenuRecipe()
            {
                MenuId = menu.MenuId,
                RecipeId = recipe.RecipeId,
            });
        }
    }
}
