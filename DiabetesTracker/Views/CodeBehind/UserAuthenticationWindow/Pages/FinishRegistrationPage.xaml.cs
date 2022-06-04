using DiabetesTracker.Logic;
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

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for FinishRegistrationForm.xaml
    /// </summary>
    public partial class FinishRegistrationPage : Page
    {
        UserAuthenticationWindow _userAuthentication;
        public FinishRegistrationPage(UserAuthenticationWindow userAuthentication)
        {
            _userAuthentication = userAuthentication;
            InitializeComponent();
        }
        private void FinishRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            UserAuthentocationLogic.FinishRegistration(_userAuthentication,Country.TextBox.Text, City.TextBox.Text, About.TextBox.Text, MeleCheckBox.IsChecked == true ? 'M' : 'F');
        }
        private void MeleCheckBox_Ckecked(object sender, RoutedEventArgs e)
        {
            MeleCheckBox.IsChecked = true;
            FemeleCheckBox.IsChecked = false;
        }
        private void FemeleCheckBox_Ckecked(object sender, RoutedEventArgs e)
        {
            MeleCheckBox.IsChecked = false;
            FemeleCheckBox.IsChecked = true;
        }
    }
}
