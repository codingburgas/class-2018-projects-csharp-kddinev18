using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public static class MenuBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Menu CreateMenu(string name, Recipe[] recipes, int userId)
        {
            Menu newMenu = new Menu()
            {
                UserId = userId,
                Name = name,
                CreatedOn = DateTime.Now
            };
            DbContext.Menus.Add(newMenu);

            foreach (Recipe recipe in recipes)
            {
                MenuRecipeBusinessLogic.AddMenuRecipes(newMenu, recipe);
            }

            DbContext.SaveChanges();

            return newMenu;
        }
    }
}
