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
    /// Interaction logic for FinishRegistrationForm.xaml
    /// </summary>
    public partial class FinishRegistrationForm : Page
    {
        DiabetesTrackerDbContext _dbContext;
        public FinishRegistrationForm(DiabetesTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
        }
        private void FinishRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (Country.Text == null || City.Text == null || About == null || (MeleCheckBox.IsChecked == false && FemeleCheckBox.IsChecked == false))
                throw new ArgumentException("You must eneter all textboxs");

            string country = Country.Text;
            string city = City.Text;
            string about = About.Text;
            char gender = MeleCheckBox.IsChecked == true? 'M' : 'F';

            UserProfile.ConfigureUserProfile(_dbContext, User.GetCurrentUser(), gender, about, country, city);
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
