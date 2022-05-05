using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace DiabetesTracker.Models
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
        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public virtual ICollection<FollowingBlog> FollowingBlogs { get; set; } = new List<FollowingBlog>();
        public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();
        public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();



        private static int? _logedUserId;
        private static string Hash(string data)
        {
            return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(data))).ToUpper().Replace("-", "");
        }
        private static string GetSalt()
        {
            byte[] salt = new byte[64];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return BitConverter.ToString(salt).ToUpper().Replace("-", "");
        }
        public static void Register(DiabetesTrackerDbContext dbContext, string userName, string email, string password)
        {
            string salt = GetSalt();
            string hashPassword = Hash(password + salt);

            foreach (User user in dbContext.Users)
                if (user.Email == email || user.UserName == userName)
                    throw new ArgumentException("There is already a user with that email or username");

            dbContext.Users.Add(new User()
            {
                UserName = userName,
                Password = hashPassword,
                Email = email,
                Salt = salt
            });

            dbContext.SaveChanges();
        }
        public static void LogIn(DiabetesTrackerDbContext dbContext, string username, string password)
        {
            List<User> users = dbContext.Users
                .Where(u => u.UserName == username)
                .ToList();

            if (users.Count == 0)
                throw new ArgumentException("Your password or username is incorrect");

            foreach (User user in users)
                if (Hash(password + user.Salt.ToString()) == user.Password)
                    _logedUserId = user.UserId;
        }
        public static int GetCurrentUser()
        {
            if (_logedUserId.HasValue)
                return _logedUserId.Value;
            else
                throw new ArgumentException("You first have to be logged");
        }
    }
}
