using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace BusinessLogicLayer
{
    public partial class UserBusinessLogic
    {
        private static int? _logedUserId;
        public static int GetCurrentUser()
        {
            return _logedUserId.Value;
        }
        public static User GetCurrentUser(DiabetesTrackerDbContext dbContext)
        {
            return dbContext.Users.Where(user => user.UserId == GetCurrentUser()).FirstOrDefault();
        }
        private static string Hash(string data)
        {
            return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(data))).ToUpper().Replace("-", "");
        }
        private static string GetSalt()
        {
            StringBuilder salt = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 16; i++)
                salt.Append(Convert.ToChar(random.Next(0, 26) + 65));
            return salt.ToString();
        }
        public static User Register(DiabetesTrackerDbContext dbContext, string userName, string email, string password)
        {
            CheckEmail(email);
            CheckPassword(password);
            string salt = GetSalt();
            string hashPassword = Hash(password + salt);

            foreach (User existingUser in dbContext.Users)
                if (existingUser.Email == email || existingUser.UserName == userName)
                    throw new ArgumentException("There is already a user with that email or username");

            User newUser = new User()
            {
                UserName = userName,
                Password = hashPassword,
                Email = email,
                Salt = salt
            };

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            _logedUserId = newUser.UserId;

            return newUser;
        }
        private static bool CheckEmail(string email)
        {
            if (email.Contains('@') == false)
                throw new ArgumentException("Email must contain \'@\'");

            return true;
        }
        private static bool CheckPassword(string pass)
        {
            if (pass.Length < 8 || pass.Length > 32)
                throw new ArgumentException("Password must be between 8 and 32 charcters");

            if (pass.Contains(" "))
                throw new ArgumentException("Password must not contain spaces");

            if (!pass.Any(char.IsUpper))
                throw new ArgumentException("Password must contain at least 1 upper character");

            if (!pass.Any(char.IsLower))
                throw new ArgumentException("Password must contain at least 1 lower character");

            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            foreach (char c in specialCharactersArray)
            {
                if (pass.Contains(c))
                    return true;
            }
            throw new ArgumentException("Password must contain at least 1 special character");
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
        public static bool CheckUserProfile(DiabetesTrackerDbContext dbContext, int userId)
        {
            return dbContext.UserProfiles.Where(userProfile => userProfile.UserId == userId).Count() == 1;
        }
    }
}
