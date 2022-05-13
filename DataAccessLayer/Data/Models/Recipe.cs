using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DataAccessLayer.Data.Models
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
    }
}
