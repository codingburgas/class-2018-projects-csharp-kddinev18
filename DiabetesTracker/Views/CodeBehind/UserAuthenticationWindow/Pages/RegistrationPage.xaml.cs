using BusinessLogicLayer;
using DiabetesTracker.Models;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private UserAuthenticationWindow _userAuthentication;
        public RegistrationPage(UserAuthenticationWindow userAuthentication)
        {
            _userAuthentication = userAuthentication;
            InitializeComponent();
        }

        private void OpenLogInFormButton_Click(object sender, RoutedEventArgs e)
        {
            _userAuthentication.ShowPage(_userAuthentication.LogInPage);
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserName.TextBox.Text;
            string email = Email.TextBox.Text;
            string password = PasswordTextBox.Password;

            try
            {
                CurrentUser.CurrentUserId = UserLogic.Register(userName, email, password);
            }
            catch (WrongCredentialsException exception)
            {
                MessageBox.Show(exception.Message, "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _userAuthentication.ShowPage(_userAuthentication.FinishRegistrationPage);
        }
    }
}
