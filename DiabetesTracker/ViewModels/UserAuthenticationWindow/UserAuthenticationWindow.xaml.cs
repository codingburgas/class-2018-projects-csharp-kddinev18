using BusinessLogicLayer;
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
    public partial class UserAuthenticationWindow : Window
    {
        private LogInForm _logInForm;
        private RegistrationForm _registrationForm;
        private FinishRegistrationForm _finishRegistrationForm;
        public UserAuthenticationWindow()
        {
            Master.OpenConnection();
            _logInForm = new LogInForm(this);
            _registrationForm = new RegistrationForm(this);
            _finishRegistrationForm = new FinishRegistrationForm(this);
            InitializeComponent();
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
        public void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
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
