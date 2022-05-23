using DiabetesTracker.Models;
using ServiceLayer;
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
            CurrentUser.CurrentUserId = UserAuthentication.LogInWithCookies();
            if(CurrentUser.CurrentUserId is not null)
            {
                _userAuthentication.OpenMainWindow();
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
                UserAuthentication.LogIn(userName, password, doRememberMe);
            }
            catch(NotFilledRequiredFieldsException exception)
            {
                MessageBox.Show(exception.Message, "User Profile data is not entered", MessageBoxButton.OK, MessageBoxImage.Information);
                _userAuthentication.ShowPage(_userAuthentication.FinishRegistrationPage);
                return;
            }
            catch(WrongCredentialsException exception)
            {
                MessageBox.Show(exception.Message, "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _userAuthentication.OpenMainWindow();
        }
    }
}
