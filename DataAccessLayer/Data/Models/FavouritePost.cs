using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class FavouritePost
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime SavedOn { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
