using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Column(TypeName = "nvarchar(1024)")]
        public string Content { get; set; }
        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PublishedOn { get; set; }

        public ICollection<PostTag> PostTags { get; set; }
    }
}
