﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiabetesTracker.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

    }
}
