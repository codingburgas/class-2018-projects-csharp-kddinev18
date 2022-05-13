using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace DataAccessLayer.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime DateRegisterd { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; } = false;
        public bool AccountStatus { get; set; } = true;

        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
        public virtual ICollection<FavouritePost> FavouritePosts { get; set; } = new List<FavouritePost>();
        public virtual ICollection<FollowingBlog> FollowingBlogs { get; set; } = new List<FollowingBlog>();
        public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();
        public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
    }
}
