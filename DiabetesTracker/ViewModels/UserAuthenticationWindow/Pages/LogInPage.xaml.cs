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
        private UserAuthentication _userAuthentication;
        public LogInForm(UserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
            if(UserAuthenticationWindow.CheckCookies())
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
            string password = PasswordTextBox.Text;

            try
            {
                UserAuthenticationWindow.LogIn(userName, password);
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

            if (RememberMeCheckBox.IsChecked == true)
            {
                UserAuthenticationWindow.AddCookies(userName, password);
            }

            _userAuthentication.OpenMainWindow();
        }
    }
}
