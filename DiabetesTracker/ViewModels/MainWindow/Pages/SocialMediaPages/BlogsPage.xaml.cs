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
    /// Interaction logic for BlogsPage.xaml
    /// </summary>
    public class BlogInformation
    {
        public BitmapImage BlogImage { get; set; }
        public string BlogName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
    }

    public partial class BlogsPage : Page
    {
        public List<BlogInformation> YourBlogsInformation { get; set; } = new List<BlogInformation>();
        public List<BlogInformation> SearchBlogsInformation { get; set; } = new List<BlogInformation>();
        public BlogsPage()
        {
            LoadBlogsInformation();
            InitializeComponent();
            YourBlogsList.ItemsSource = YourBlogsInformation;
        }
        private void LoadBlogsInformation()
        {
            List<Tuple<BitmapImage, string, int, int>> currentUserblogsInformation = MainWindowModel.GetCurrentUserBlogsInformation();
            foreach (Tuple<BitmapImage, string, int, int> currentUserblogInformation in currentUserblogsInformation)
            {
                YourBlogsInformation.Add(new BlogInformation() 
                {
                    BlogImage = currentUserblogInformation.Item1,
                    BlogName = currentUserblogInformation.Item2,
                    PostCount = currentUserblogInformation.Item3,
                    FollowingCount = currentUserblogInformation.Item4,
                });
            }
        }
        private void LoadBlogsInformation(string blogName)
        {
            SearchBlogsInformation = new List<BlogInformation>();
            List<Tuple<BitmapImage, string, int, int>> searchBlogsInformation = MainWindowModel.GetBlogsInformationByName(blogName);
            foreach (Tuple<BitmapImage, string, int, int> searchBlogInformation in searchBlogsInformation)
            {
                SearchBlogsInformation.Add(new BlogInformation()
                {
                    BlogImage = searchBlogInformation.Item1,
                    BlogName = searchBlogInformation.Item2,
                    PostCount = searchBlogInformation.Item3,
                    FollowingCount = searchBlogInformation.Item4,
                });
            }
        }

        //Event handlers
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (BlogName.TextBox.Text == "")
            {
                SearchBlogsInformation = new List<BlogInformation>();
            }
            else
            {
                string blogName = BlogName.TextBox.Text;
                BlogName.TextBox.Text = "";
                LoadBlogsInformation(blogName);
            }
            SerchBlogsList.ItemsSource = SearchBlogsInformation;
        }
    }
}
