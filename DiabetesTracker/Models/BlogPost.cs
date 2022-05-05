using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class BlogPost
    {
        public int BlogId { get; set; }
        public int PostId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Post Post { get; set; }
    }
}
