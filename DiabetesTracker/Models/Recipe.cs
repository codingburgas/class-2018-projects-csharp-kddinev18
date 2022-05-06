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
            dbContext.Recipes.Add(new Recipe() 
            { 
                UserId = User.GetCurrentUser(),
                Name = name,
                Content = content,
                CreatedOn = DateTime.Now,
            });

            dbContext.SaveChanges();

            return dbContext.Recipes.GroupBy(recipe => recipe.RecipeId).Last().ToList()[0];
        }
    }
}
