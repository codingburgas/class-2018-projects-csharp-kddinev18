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
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException() { }

        public WrongCredentialsException(string message) : base(message) { }

        public WrongCredentialsException(string message, Exception inner) : base(message, inner) { }
    }
    public static class UserBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        private static int? _logedUserId;
        public static int GetCurrentUserId()
        {
            return _logedUserId.Value;
        }
        public static User GetCurrentUser()
        {
            return DbContext.Users.Where(user => user.UserId == GetCurrentUserId()).FirstOrDefault();
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
        public static User Register(string userName, string email, string password)
        {
            CheckEmail(email);
            CheckPassword(password);
            string salt = GetSalt();
            string hashPassword = Hash(password + salt);

            foreach (User existingUser in DbContext.Users)
                if (existingUser.Email == email || existingUser.UserName == userName)
                    throw new WrongCredentialsException("There is already a user with that email or username");

            User newUser = new User()
            {
                UserName = userName,
                Password = hashPassword,
                Email = email,
                Salt = salt
            };

            DbContext.Users.Add(newUser);
            DbContext.SaveChanges();

            _logedUserId = newUser.UserId;

            return newUser;
        }
        private static bool CheckEmail(string email)
        {
            if (email.Contains('@') == false)
                throw new WrongCredentialsException("Email must contain \'@\'");

            return true;
        }
        private static bool CheckPassword(string pass)
        {
            if (pass.Length < 8 || pass.Length > 32)
                throw new WrongCredentialsException("Password must be between 8 and 32 charcters");

            if (pass.Contains(" "))
                throw new WrongCredentialsException("Password must not contain spaces");

            if (!pass.Any(char.IsUpper))
                throw new WrongCredentialsException("Password must contain at least 1 upper character");

            if (!pass.Any(char.IsLower))
                throw new WrongCredentialsException("Password must contain at least 1 lower character");

            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            foreach (char c in specialCharactersArray)
            {
                if (pass.Contains(c))
                    return true;
            }
            throw new WrongCredentialsException("Password must contain at least 1 special character");
        }

        public static string LogIn(string username, string password)
        {
            List<User> users = DbContext.Users
                .Where(u => u.UserName == username)
                .ToList();

            if (users.Count == 0)
                throw new WrongCredentialsException("Your password or username is incorrect");

            foreach (User user in users)
            {
                if (Hash(password + user.Salt.ToString()) == user.Password)
                {
                    _logedUserId = user.UserId;
                    return Hash(password + user.Salt.ToString());
                }
            }
            throw new WrongCredentialsException("Your password or username is incorrect");
        }
        public static void LogInWithPreHashedPassword(string username, string preHashedPassword)
        {
            List<User> users = DbContext.Users
                .Where(u => u.UserName == username)
                .ToList();

            if (users.Count == 0)
                throw new WrongCredentialsException("Your password or username is incorrect");

            foreach (User user in users)
            {
                if (preHashedPassword == user.Password)
                {
                    _logedUserId = user.UserId;
                }
            }
            throw new WrongCredentialsException("Your password or username is incorrect");
        }
        public static bool CheckUserProfile(int userId)
        {
            return DbContext.UserProfiles.Where(userProfile => userProfile.UserId == userId).Count() == 1;
        }
    }
}
