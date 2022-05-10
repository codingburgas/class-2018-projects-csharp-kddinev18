using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class MenuRecipe
    {
        public int MenuRecipeId { get; set; }
        public int MenuId { get; set; }
        public int RecipeId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Recipe Recipe { get; set; }

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
