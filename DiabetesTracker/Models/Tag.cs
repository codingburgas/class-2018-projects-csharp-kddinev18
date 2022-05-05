using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Column(TypeName = "nvarchar(64)")]
        public string TagName { get; set; }
    }
}
