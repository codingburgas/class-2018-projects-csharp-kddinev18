using System;
using System.Collections.Generic;
using System.Windows.Media;
#nullable disable

namespace DiabetesTracker.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public ImageSource Image { get; set; }
        public DateTime PublishedOn { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();
        public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        public static Post AddPost(DiabetesTrackerDbContext dbContext, string content, ImageSource image, Tag[] tags)
        {
            dbContext.Posts.Add(new Post() 
            { 
                Content = content,
                User = User.GetCurrentUser(),
                Image = image,
                PublishedOn = DateTime.Now
            });
            Post post = dbContext.Posts.GroupBy(post=>post.PostId);
            foreach (Tag tag in tags)
	        {
                PostTag.AddPostTag(dbContext, )
	        }
        }
    }
}
