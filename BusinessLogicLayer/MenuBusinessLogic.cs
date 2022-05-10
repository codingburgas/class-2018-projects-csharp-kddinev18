using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogicLayer
{
    public class MenuBusinessLogic
    {
        public static Menu CreateMenu(DiabetesTrackerDbContext dbContext, string name, Recipe[] recipes)
        {
            Menu newMenu = new Menu()
            {
                UserId = User.GetCurrentUser(),
                Name = name,
                CreatedOn = DateTime.Now
            };
            dbContext.Menus.Add(newMenu);

            foreach (Recipe recipe in recipes)
            {
                MenuRecipe.AddMenuRecipes(dbContext, newMenu, recipe);
            }

            dbContext.SaveChanges();

            return newMenu;
        }
    }
}
