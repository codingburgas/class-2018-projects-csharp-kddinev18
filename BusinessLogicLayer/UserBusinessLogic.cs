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
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException() { }

        public WrongCredentialsException(string message) : base(message) { }

        public WrongCredentialsException(string message, Exception inner) : base(message, inner) { }
    }
    public class UserCredentials
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
    public static class UserBusinessLogic
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

            return newUser.UserId;
        }
        private static bool CheckEmail(string email)
        {
            if (email.Contains('@') == false)
                throw new WrongCredentialsException("Email must contain \'@\'");

            return true;
        }
        private static bool CheckPassword(string pass)
        {
            if (pass.Length <= 10 || pass.Length > 32)
                throw new WrongCredentialsException("Password must be between 10 and 32 charcters");

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

        private static UserCredentials LogIn(string username, string password)
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
                    return new UserCredentials() 
                    {
                        Id = user.UserId,
                        UserName = user.UserName,
                        HashedPassword = user.Password,
                    };
                }
            }
            throw new WrongCredentialsException("Your password or username is incorrect");
        }
        private static UserCredentials LogInWithPreHashedPassword(string username, string preHashedPassword)
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
                    return new UserCredentials() 
                    { 
                        Id = user.UserId,
                        UserName = user.UserName,
                        HashedPassword = user.Password
                    };
                }
            }
            throw new WrongCredentialsException("Your password or username is incorrect");
        }
        public static bool CheckUserProfile(int userId)
        {
            return DbContext.UserProfiles.Where(userProfile => userProfile.UserId == userId).Count() == 1;
        }
        private readonly static string _userCredentialsPath = @$"{Directory.GetCurrentDirectory()}/DiabetesTrackerCredentials.txt";
        public static int LogIn(string userName, string password, bool doRememberMe)
        {
            UserCredentials userCredentials = LogIn(userName, password);

            if (CheckUserProfile(userCredentials.Id) == false)
                throw new ArgumentNullException(" You need to finish your registration first");

            if (doRememberMe)
                AddCookies(userCredentials.Id, userCredentials.UserName, userCredentials.HashedPassword);
            else
                RemoveCookies();

            return userCredentials.Id;
        }
        public static void AddCookies(int userId ,string userName, string hashedPassword)
        {
            File.WriteAllText(_userCredentialsPath, JsonSerializer.Serialize(new UserCredentials() { Id = userId, UserName = userName, HashedPassword = hashedPassword }));
        }
        public static int CheckCookies()
        {
            if (!File.Exists(_userCredentialsPath))
                return -1;

            string credentials = File.ReadAllText(_userCredentialsPath);
            UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(credentials);
            LogInWithPreHashedPassword(userCredentials.UserName, userCredentials.HashedPassword);
            return userCredentials.Id;
        }
        public static void RemoveCookies()
        {
            if (!File.Exists(_userCredentialsPath))
                return;

            File.Delete(_userCredentialsPath);
        }
    }
}
