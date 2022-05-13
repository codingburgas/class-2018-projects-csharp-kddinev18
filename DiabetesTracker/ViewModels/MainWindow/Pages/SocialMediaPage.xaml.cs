using DiabetesTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class PostInfomation
    {
        public string UserName { get; set; }
        public System.Drawing.Image Image { get; set; }
        public string Content { get; set; }
     }
    public partial class SocialMediaPage : Page
    {
        public ObservableCollection<PostInfomation> Posts { get; set; } = new ObservableCollection<PostInfomation>();
        public SocialMediaPage()
        {
            InitializeComponent();
            Posts.Add(MainWindowModel.GetPost());
            Posts.Add(MainWindowModel.GetPost());
            Posts.Add(MainWindowModel.GetPost());
            PostsList.ItemsSource = Posts;
        }
    }
}
