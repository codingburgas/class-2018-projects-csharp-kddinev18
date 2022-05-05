using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string UserName { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(8)")]
        public string Salt { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsAdmin { get; set; }
        public bool AccountStatus { get; set; }

        public ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
