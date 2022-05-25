using DataAccessLayer;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

#nullable disable

namespace BusinessLogicLayer
{
    public class UserCredentials
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
    public static class UserLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
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
        public static int Register(string userName, string email, string password)
        {
            CheckEmail(email);
            CheckPassword(password);
            string salt = GetSalt();
            string hashPassword = Hash(password + salt);

            foreach (User existingUser in DbContext.Users)
                if (existingUser.Email == email || existingUser.UserName == userName)
                    throw new ArgumentException("There is already a user with that email or username");

            User newUser = new User()
            {
                UserName = userName,
                Password = hashPassword,
                Email = email,
                Salt = salt
            };

            DbContext.Users.Add(newUser);
            DbContext.SaveChanges();

            return newUser.UserId;
        }
        private static bool CheckEmail(string email)
        {
            if (email.Contains('@') == false)
                throw new ArgumentException("Email must contain \'@\'");

            return true;
        }
        private static bool CheckPassword(string pass)
        {
            if (pass.Length <= 10 || pass.Length > 32)
                throw new ArgumentException("Password must be between 10 and 32 charcters");

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
        public static int LogInWithPreHashedPassword(string username, string preHashedPassword)
        {
            List<User> users = DbContext.Users
                .Where(u => u.UserName == username)
                .ToList();

            if (users.Count == 0)
                throw new ArgumentException("Your password or username is incorrect");

            foreach (User user in users)
            {
                if (preHashedPassword == user.Password)
                {
                    return user.UserId;
                }
            }
            throw new ArgumentException("Your password or username is incorrect");
        }
        public static bool CheckUserProfile(int userId)
        {
            return DbContext.UserProfiles.Where(userProfile => userProfile.UserId == userId).Count() == 1;
        }
        public static UserCredentials LogIn(string userName, string password)
        {
            UserCredentials userCredentials;
            List<User> users = DbContext.Users
                .Where(u => u.UserName == userName)
                .ToList();

            if (users.Count == 0)
                throw new ArgumentException("Your password or username is incorrect");

            foreach (User user in users)
            {
                if (Hash(password + user.Salt.ToString()) == user.Password)
                {
                    userCredentials = new UserCredentials()
                    {
                        Id = user.UserId,
                        UserName = user.UserName,
                        HashedPassword = user.Password,
                    };

                    if (CheckUserProfile(userCredentials.Id) == false)
                        throw new ArgumentNullException("You need to finish your registration first");

                    return userCredentials;
                }
            }
            throw new ArgumentException("Your password or username is incorrect");
        }
    }
}
