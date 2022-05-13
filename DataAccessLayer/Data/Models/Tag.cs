using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
