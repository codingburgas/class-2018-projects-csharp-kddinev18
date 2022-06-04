using DiabetesTracker.Models;
using DiabetesTracker.Views;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiabetesTracker.Logic
{
    public static class UserAuthentocationLogic
    {
        public static void FinishRegistration(UserAuthenticationWindow userAuthentication, string country, string city, string about, char gender)
        {
            if (!HandleUserInput.GeneralHandler(country, city, about))
                return;

            try
            {
                Services.FinishRegistration(CurrentUserInformation.CurrentUserId.Value, gender, about, country, city);
                userAuthentication.OpenMainWindow();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void LogIn(UserAuthenticationWindow userAuthentication, string userName, string password, bool doRememberMe)
        {
            if (!HandleUserInput.GeneralHandler(userName, password))
                return;

            try
            {
                CurrentUserInformation.CurrentUserId = Services.LogIn(userName, password, doRememberMe);
                userAuthentication.OpenMainWindow();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static void Register(UserAuthenticationWindow userAuthentication, string userName, string email, string password)
        {
            if (!HandleUserInput.GeneralHandler(userName, email, password))
                return;

            try
            {
                CurrentUserInformation.CurrentUserId = Services.Register(userName, email, password);
                userAuthentication.ShowPage(userAuthentication.FinishRegistrationPage);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
