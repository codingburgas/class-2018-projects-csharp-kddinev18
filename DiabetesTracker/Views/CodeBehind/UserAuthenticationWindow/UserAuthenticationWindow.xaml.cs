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

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for UserAuthentication.xaml
    /// </summary>
    public partial class UserAuthenticationWindow : Window
    {
        public LogInPage LogInPage { get; set; }
        public RegistrationPage RegistrationPage { get; set; }
        public FinishRegistrationPage FinishRegistrationPage { get; set; }
        public UserAuthenticationWindow()
        {
            Services.SetUpConnection();
            LogInPage = new LogInPage(this);
            RegistrationPage = new RegistrationPage(this);
            FinishRegistrationPage = new FinishRegistrationPage(this);
            InitializeComponent();
            ShowPage(LogInPage);
        }

        public void ShowPage(Page page)
        {
            Forms.Content = page;
        }
        public void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        //Event handlers
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Services.RemoveConnection();
            Application.Current.Shutdown();
        }
    }
}
