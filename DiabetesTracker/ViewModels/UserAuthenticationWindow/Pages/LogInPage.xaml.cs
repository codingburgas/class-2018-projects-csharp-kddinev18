using BusinessLogicLayer;
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
    public class UserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public partial class LogInForm : Page
    {
        private UserAuthentication _userAuthentication;
        private readonly string _userCredentialsPath = @$"C:\Users\{WindowsIdentity.GetCurrent().Name}\AppData\Local\Microsoft\Windows\INetCookies\DiabetesTracker.txt";
        public LogInForm(UserAuthentication userAuthentication)
        {
            if(File.Exists(_userCredentialsPath))
            {
                string credentials = File.ReadAllText(_userCredentialsPath);
                UserCredentials userCredentials = JsonSerializer.Deserialize<UserCredentials>(credentials);
                UserBusinessLogic.LogIn(userCredentials.UserName, userCredentials.Password);
            }
            _userAuthentication = userAuthentication;
            InitializeComponent();
        }
        private void OpenRegistrationFromButton_Click(object sender, RoutedEventArgs e)
        {
            _userAuthentication.ShowRegisterForm();
        }
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Text;

            try
            {
                UserBusinessLogic.LogIn(userName, password);
            }catch (WrongCredentialsException exception)
            {
                MessageBox.Show(exception.Message, "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UserBusinessLogic.CheckUserProfile(UserBusinessLogic.GetCurrentUserId()) == false)
                _userAuthentication.ShowFinishRegisterForm();

            if(RememberMeCheckBox.IsChecked == true)
            {
                File.WriteAllText(_userCredentialsPath, JsonSerializer.Serialize(new UserCredentials() { UserName = userName, Password = password}));
            }

            _userAuthentication.OpenMainWindow();
        }
    }
}
