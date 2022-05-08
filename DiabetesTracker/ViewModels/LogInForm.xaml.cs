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
    /// Interaction logic for LogInForm.xaml
    /// </summary>
    public partial class LogInForm : Page
    {
        private DiabetesTrackerDbContext _dbContext;
        private UserAuthentication _userAuthentication;
        public LogInForm(DiabetesTrackerDbContext dbContext, UserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
            _dbContext = dbContext;
            InitializeComponent();
        }

        private void OpenRegistrationFromButton_Click(object sender, RoutedEventArgs e)
        {
            _userAuthentication.ShowRegisterForm();
        }
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTextBox.Text == null || PasswordTextBox.Text == null)
                throw new ArgumentException("Wrong username or password");

            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Text;

            User.LogIn(_dbContext, userName, password);

            if (User.CheckUserProfile(_dbContext, User.GetCurrentUser()) == false)
                _userAuthentication.ShowFinishRegisterForm();
        }
    }
}
