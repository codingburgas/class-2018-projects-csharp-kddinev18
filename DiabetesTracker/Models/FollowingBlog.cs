using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class FollowingBlog
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public int FollowedOn { get; set; }

        public virtual User FollowedOnNavigation { get; set; }
        public virtual Blog User { get; set; }
    }
}
