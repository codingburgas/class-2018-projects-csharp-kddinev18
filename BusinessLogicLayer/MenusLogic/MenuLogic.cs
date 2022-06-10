using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public static class MenuLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        
        // Adds a Menu and automatically adds the Recipes from the menu into the dabase
        public static Menu CreateMenu(string name, Recipe[] recipes, int userId)
        {
            // Create new instance of a Menu
            Menu newMenu = new Menu()
            {
                UserId = userId,
                Name = name
            };
            // Add the instance of the Menu into the context
            DbContext.Menus.Add(newMenu);

            // Add all recipes from the Menu into the context
            foreach (Recipe recipe in recipes)
            {
                MenuRecipeLogic.AddMenuRecipes(newMenu, recipe);
            }

            // Save all changes made in this context into the database
            DbContext.SaveChanges();

            return newMenu;
        }
    }
}
