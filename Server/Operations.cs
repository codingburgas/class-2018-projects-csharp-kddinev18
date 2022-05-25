using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    public static class Operations
    {
        public static int Register(string userName, string email, string password)
        {
            return UserLogic.Register(userName, email, password);
        }

        public static void FinishRegistration(int userId, char gender, string about, string country, string city)
        {
            UserProfileLogic.ConfigureUserProfile(userId, gender, about, country, city);
        }

        public static string LogIn(string userName, string password)
        {
            return JsonSerializer.Serialize(UserLogic.LogIn(userName, password));
        }

        public static int LogInWithCookies(string userName, string password)
        {
            return UserLogic.LogInWithPreHashedPassword(userName, password);
        }

        public static string GetPosts(int userId, int skipCount)
        {
            return JsonSerializer.Serialize(PostLogic.ArrangePosts(userId, skipCount));
        }
    }
}
