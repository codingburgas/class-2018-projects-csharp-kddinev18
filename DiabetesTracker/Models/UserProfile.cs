using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Column(TypeName = "char")]
        public char Gender { get; set; }
        [Column(TypeName = "text")]
        public string About { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Country { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string City { get; set; }
    }
}
