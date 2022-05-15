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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SocialMediaPage _socialMediaPage;
        public MainWindow()
        {
            _socialMediaPage = new SocialMediaPage();
            InitializeComponent();
            MainWindowFrame.Content = _socialMediaPage;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            UserAuthenticationWindowModel.RemoveCookies();
            new UserAuthenticationWindow().Show();
            this.Close();
        }
    }
}
