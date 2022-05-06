using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public partial class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public char Gender { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public virtual User User { get; set; }
    }
}
