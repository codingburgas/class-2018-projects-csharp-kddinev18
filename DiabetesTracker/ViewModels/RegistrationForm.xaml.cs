﻿using System;
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
        private UserAuthentication _userAuthentication;
        public RegistrationForm(UserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameRegisterTextBox.Text == null || PasswordRegisterTextBox.Text == null || EmailRegisterTextBox == null)
                throw new ArgumentException("Wrong username or password");

            string userName = UserNameRegisterTextBox.Text;
            string email = EmailRegisterTextBox.Text;
            string password = PasswordRegisterTextBox.Text;

            //register

            _userAuthentication.ShowFinishRegisterForm();
        }
        private void OpenLogInFromButton_Click(object sender, RoutedEventArgs e)
        {
            _userAuthentication.ShowLogInForm();
        }
    }
}