using DiabetesTracker.Models;
using DiabetesTracker.Views;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
#nullable disable

namespace DiabetesTracker.Logic
{
    public static class UserAuthentocationLogic
    {
        // Add user profile
        public static void FinishRegistration(UserAuthenticationWindow userAuthentication, string country, string city, string about, char gender)
        {
            //Checks the user input
            if (!HandleUserInput.GeneralHandler(country, city, about))
                return;

            try
            {
                // Add user profile
                Services.FinishRegistration(CurrentUserInformation.CurrentUserId.Value, gender, about, country, city);
                // Open the main window
                userAuthentication.OpenMainWindow();
            }
            catch (Exception exception)
            {
                // Show error message box
                MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public static void LogIn(UserAuthenticationWindow userAuthentication, string userName, string password, bool doRememberMe)
        {
            //Checks the user input
            if (!HandleUserInput.GeneralHandler(userName, password))
                return;

            try
            {
                // Log in and assign the current user id to the id of the user that has just logged
                CurrentUserInformation.CurrentUserId = Services.LogIn(userName, password, doRememberMe);
                // Open the main window
                userAuthentication.OpenMainWindow();
            }
            catch (Exception exception)
            {
                // Show error message box
                MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public static void Register(UserAuthenticationWindow userAuthentication, string userName, string email, string password)
        {
            //Checks the user input
            if (!HandleUserInput.GeneralHandler(userName, email, password))
                return;

            try
            {
                // Register and assign the current user id to the id of the user that has just registered
                CurrentUserInformation.CurrentUserId = Services.Register(userName, email, password);
                // Show FinishRegistrationPage
                userAuthentication.ShowPage(userAuthentication.FinishRegistrationPage);
            }
            catch (Exception exception)
            {
                // Show error message box
                MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
