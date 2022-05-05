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
    }
}
