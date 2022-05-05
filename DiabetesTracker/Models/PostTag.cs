using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public class PostTag
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
