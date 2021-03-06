using BusinessLogicLayer;
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
using System.Windows.Shapes;

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SocialMediaPage SocialMediaPage { get; set; }
        public MainWindow()
        {
            // Start up the server connection
            Services.SetUpConnection();
            SocialMediaPage = new SocialMediaPage(this);
            InitializeComponent();
            // Show SocialMediaPage
            ShowPage(SocialMediaPage);
        }

        public void ShowPage(Page page)
        {
            MainWindowFrame.Content = page;
        }

        //Event handlers
        private void LogOutButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Remove user credentials file 
            Services.RemoveCookies();
            // Remove the server connection
            Services.RemoveConnection();
            // Show new UserAuthenticationWindow
            new UserAuthenticationWindow().Show();
            // Close this window
            this.Close();
        }
        private void ExitButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Remove the server connection
            Services.RemoveConnection();
            // Shutdown the application
            Application.Current.Shutdown();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Drag the window of the button is hold
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
