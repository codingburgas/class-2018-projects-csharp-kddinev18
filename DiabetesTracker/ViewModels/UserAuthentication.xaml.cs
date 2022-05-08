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
        private LogInForm _logInForm;
        private RegistrationForm _registrationForm;
        private FinishRegistrationForm _finishRegistrationForm;
        public UserAuthentication()
        {
            _dbContext = new DiabetesTrackerDbContext();
            InitializeComponent();
            _logInForm = new LogInForm(_dbContext, this);
            _registrationForm = new RegistrationForm(_dbContext, this);
            _finishRegistrationForm = new FinishRegistrationForm(_dbContext);
            ShowLogInForm();
        }
        public void ShowLogInForm()
        {
            Forms.Content = _logInForm;
        }
        public void ShowRegisterForm()
        {
            Forms.Content = _registrationForm;
        }
        public void ShowFinishRegisterForm()
        {
            Forms.Content = _finishRegistrationForm;
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
