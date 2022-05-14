using BusinessLogicLayer;
using DiabetesTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for SocialMediaPage.xaml
    /// </summary>
    public partial class SocialMediaPage : Page
    {
        public SocialMediaPage()
        {
            InitializeComponent();
            SocialMediaPageFrame.Content = new PostsPage();
        }
    }
}
