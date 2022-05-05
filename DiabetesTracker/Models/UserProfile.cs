using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public partial class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public virtual User User { get; set; }
    }
}
