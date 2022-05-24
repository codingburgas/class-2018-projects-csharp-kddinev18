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

namespace DiabetesTracker.ViewModels
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
            string country = Country.TextBox.Text;
            string city = City.TextBox.Text;
            string about = About.TextBox.Text;
            char gender = MeleCheckBox.IsChecked == true ? 'M' : 'F';

            try
            {
                Services.FinishRegistration(CurrentUserInformation.CurrentUserId.Value, gender, about, country, city);
                _userAuthentication.OpenMainWindow();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
