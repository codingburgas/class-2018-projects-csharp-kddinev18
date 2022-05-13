using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class PostComment
    {
        public int PostCommentsId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string CommentContend { get; set; }
        public DateTime CommentedOn { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
