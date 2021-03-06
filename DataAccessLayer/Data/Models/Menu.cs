using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();
    }
}
