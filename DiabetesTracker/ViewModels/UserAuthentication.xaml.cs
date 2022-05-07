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
using System.Windows.Shapes;

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for UserAuthentication.xaml
    /// </summary>
    public partial class UserAuthentication : Window
    {
        private DiabetesTrackerDbContext _dbContext;
        public UserAuthentication()
        {
            _dbContext = new DiabetesTrackerDbContext();
            InitializeComponent();
        }
        private void OpenRegistrationFromButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameRegisterTextBox.Text == null || PasswordRegisterTextBox.Text == null || EmailRegisterTextBox == null)
                throw new ArgumentException("Wrong username or password");

            string userName = UserNameRegisterTextBox.Text;
            string email = EmailRegisterTextBox.Text;
            string password = PasswordRegisterTextBox.Text;

            User.Register(_dbContext, userName, email, password);
        }
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTextBox.Text == null || PasswordTextBox.Text == null)
                throw new ArgumentException("Wrong username or password");

            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Text;

            User.LogIn(_dbContext, userName, password);
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
