using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiabetesTracker.Models
{
    public class UserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public static class UserAuthenticationWindowModel
    {
        private readonly static string _userCredentialsPath = @$"{Directory.GetCurrentDirectory()}/DiabetesTrackerCredentials.txt";
        public static void LogIn(string userName, string password, bool doRememberMe)
        {
            string hashedPassword = UserBusinessLogic.LogIn(userName, password);

            if (doRememberMe)
                AddCookies(userName, hashedPassword);
            else
                RemoveCookies();

            if (UserBusinessLogic.CheckUserProfile(UserBusinessLogic.GetCurrentUserId()) == false)
                throw new ArgumentNullException(" You need to finish your refistration first");
        }
        public static void AddCookies(string userName, string hashedPassword)
        {
            File.WriteAllText(_userCredentialsPath, JsonSerializer.Serialize(new UserCredentials() { UserName = userName, Password = hashedPassword }));
        }
        public static bool CheckCookies()
        {
            if (!File.Exists(_userCredentialsPath))
                return false;

            string credentials = File.ReadAllText(_userCredentialsPath);
            UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(credentials);
            UserBusinessLogic.LogInWithPreHashedPassword(userCredentials.UserName, userCredentials.Password);
            return true;
        }
        public static void RemoveCookies()
        {
            if (!File.Exists(_userCredentialsPath))
                return;

            File.Delete(_userCredentialsPath);
        }
        public static void Register(string userName, string email, string password)
        {
            UserBusinessLogic.Register(userName, email, password);
        }
        public static void FinishRegistration(char gender, string about, string country, string city)
        {
            UserProfileBusinessLogic.ConfigureUserProfile(UserBusinessLogic.GetCurrentUserId(), gender, about, country, city);
        }
    }
}
