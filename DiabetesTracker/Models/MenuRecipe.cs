using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class MenuRecipe
    {
        public int MenuRecipeId { get; set; }
        public int MenuId { get; set; }
        public int RecipeId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
