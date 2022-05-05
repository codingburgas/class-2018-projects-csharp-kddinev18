using System;
using System.Collections.Generic;

#nullable disable

namespace DiabetesTracker.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public DateTime PublishedOn { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();
        public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
