using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiabetesTracker.Models
{
    public partial class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

    }
}
