﻿using BusinessLogicLayer;
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
    public partial class LogInForm : Page
    {
        private UserAuthenticationWindow _userAuthentication;
        public LogInForm(UserAuthenticationWindow userAuthentication)
        {
            _userAuthentication = userAuthentication;
            if(UserAuthenticationWindowModel.CheckCookies())
            {
                _userAuthentication.OpenMainWindow();
            }
            InitializeComponent();
        }
        private void OpenRegistrationFromButton_Click(object sender, RoutedEventArgs e)
        {
            _userAuthentication.ShowRegisterForm();
        }
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Password;
            bool doRememberMe;
            if (RememberMeCheckBox.IsChecked == true)
                doRememberMe = true;
            else
                doRememberMe = false;

            try
            {
                UserAuthenticationWindowModel.LogIn(userName, password, doRememberMe);
            }
            catch (WrongCredentialsException exception)
            {
                MessageBox.Show(exception.Message, "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (ArgumentNullException)
            {
                _userAuthentication.OpenMainWindow();
            }

            _userAuthentication.OpenMainWindow();
        }
    }
}
