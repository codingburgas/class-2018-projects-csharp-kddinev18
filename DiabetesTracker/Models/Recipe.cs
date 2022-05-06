using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DiabetesTracker.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();

        public static Recipe CreateRecipe(DiabetesTrackerDbContext dbContext, string name, string content)
        {
            Recipe newRecipe = new Recipe() 
            {
                UserId = User.GetCurrentUser(),
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
