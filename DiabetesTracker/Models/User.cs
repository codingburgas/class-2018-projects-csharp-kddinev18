using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Salt { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsAdmin { get; set; }
        public bool AccountStatus { get; set; }
    }
}
