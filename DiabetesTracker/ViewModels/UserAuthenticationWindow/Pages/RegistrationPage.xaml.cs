using BusinessLogicLayer;
using DiabetesTracker.Models;
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
    public partial class RegistrationForm : Page
    {
        private UserAuthenticationWindow _userAuthentication;
        public RegistrationForm(UserAuthenticationWindow userAuthentication)
        {
            _userAuthentication = userAuthentication;
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameRegisterTextBox.Text;
            string email = EmailRegisterTextBox.Text;
            string password = PasswordRegisterTextBox.Text;

            try
            {
                UserAuthenticationWindowModel.Register(userName, email, password);
            }
            catch (WrongCredentialsException exception)
            {
                MessageBox.Show(exception.Message, "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _userAuthentication.ShowFinishRegisterForm();
        }
        private void OpenLogInFromButton_Click(object sender, RoutedEventArgs e)
        {
            _userAuthentication.ShowLogInForm();
        }
    }
}
