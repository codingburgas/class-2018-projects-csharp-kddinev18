using DiabetesTracker.Logic;
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

namespace DiabetesTracker.Views
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
            CurrentUserInformation.CurrentUserId = Services.LogInWithCookies();
            if (CurrentUserInformation.CurrentUserId is not null)
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
            UserAuthentocationLogic.LogIn(_userAuthentication, UserName.TextBox.Text, PasswordTextBox.Password, RememberMeCheckBox.IsChecked == true ? true : false);
        }
    }
}
