using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DiabetesTracker.Models
{
    public partial class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
    }
}
