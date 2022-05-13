using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class FollowingBlog
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public DateTime FollowedOn { get; set; }

        public virtual User User { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
