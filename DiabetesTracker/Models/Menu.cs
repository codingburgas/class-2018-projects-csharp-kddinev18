using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();

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
