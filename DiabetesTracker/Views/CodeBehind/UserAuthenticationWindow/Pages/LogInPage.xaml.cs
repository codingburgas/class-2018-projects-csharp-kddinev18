using BusinessLogicLayer;
using DiabetesTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for LogInForm.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private UserAuthenticationWindow _userAuthentication;
        public LogInPage(UserAuthenticationWindow userAuthentication)
        {
            _userAuthentication = userAuthentication;
            CurrentUser.CurrentUserId = UserBusinessLogic.CheckCookies();
            if (CurrentUser.CurrentUserId != -1)
            {
                _userAuthentication.OpenMainWindow();
            }
            else
            {
                CurrentUser.CurrentUserId = null;
            }
            InitializeComponent();
        }

        //Event handlers
        private void OpenRegistrationFormButton_Click(object sender, RoutedEventArgs e)
        {
            _userAuthentication.ShowPage(_userAuthentication.RegistrationPage);
        }
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserName.TextBox.Text;
            string password = PasswordTextBox.Password;
            bool doRememberMe = RememberMeCheckBox.IsChecked == true ? true : false;

            try
            {
                CurrentUser.CurrentUserId = UserBusinessLogic.LogIn(userName, password, doRememberMe);

            }
            catch (WrongCredentialsException exception)
            {
                MessageBox.Show(exception.Message, "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (ArgumentNullException exception)
            {
                MessageBox.Show(exception.Message, "User Profile data is not entered", MessageBoxButton.OK, MessageBoxImage.Information);
                _userAuthentication.ShowPage(_userAuthentication.FinishRegistrationPage);
                return;
            }

            _userAuthentication.OpenMainWindow();
        }
    }
}
