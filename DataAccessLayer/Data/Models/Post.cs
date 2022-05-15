using System;
using System.Collections.Generic;
using System.Linq;
#nullable disable

namespace DataAccessLayer.Data.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public DateTime PublishedOn { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public virtual User User { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual ICollection<FavouritePost> FavouritePosts { get; set; } = new List<FavouritePost>();
        public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();
        public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
