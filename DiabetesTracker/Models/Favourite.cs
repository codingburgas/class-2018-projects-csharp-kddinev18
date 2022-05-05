using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class Favourite
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime SavedOn { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
