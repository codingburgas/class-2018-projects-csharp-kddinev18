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
    public static class UserAuthenticationWindow
    {
        private readonly static string _userCredentialsPath = @$"{Directory.GetCurrentDirectory()}/DiabetesTrackerCredentials.txt";
        public static void LogIn(string userName, string password)
        {
            UserBusinessLogic.LogIn(userName, password);

            if (UserBusinessLogic.CheckUserProfile(UserBusinessLogic.GetCurrentUserId()) == false)
                throw new ArgumentNullException("User Profile data is not entered");
        }
        public static void AddCookies(string userName, string password)
        {
            File.WriteAllText(_userCredentialsPath, JsonSerializer.Serialize(new UserCredentials() { UserName = userName, Password = password }));
        }
        public static bool CheckCookies()
        {
            if (!File.Exists(_userCredentialsPath))
                return false;

            string credentials = File.ReadAllText(_userCredentialsPath);
            UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(credentials);
            UserBusinessLogic.LogIn(userCredentials.UserName, userCredentials.Password);
            return true;
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
