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
    /// Interaction logic for BlogsPage.xaml
    /// </summary>
    public partial class BlogsPage : Page
    {
        public ObservableCollection<BlogInformation> YourBlogsInformation { get; set; } = new ObservableCollection<BlogInformation>();
        public ObservableCollection<BlogInformation> SearchBlogsInformation { get; set; } = new ObservableCollection<BlogInformation>();
        public BlogInformation SelectedBlogInformation { get; set; } = new BlogInformation();
        public BlogsPage()
        {
            LoadBlogsInformation();
            InitializeComponent();
            DataContext = this;
        }
        private void LoadBlogsInformation()
        {
            List<Tuple<byte[], string, int, int>> currentUserblogsInformation = BlogLogic.GetCurrentUserBlogsInformation(CurrentUser.CurrentUserId.Value);
            foreach (Tuple<byte[], string, int, int> currentUserblogInformation in currentUserblogsInformation)
            {
                YourBlogsInformation.Add(new BlogInformation() 
                {
                    BlogImage = new BitmapImage(),//ConvertByteArrayToBitMapImage(currentUserblogInformation.Item1),
                    BlogName = currentUserblogInformation.Item2,
                    PostCount = currentUserblogInformation.Item3,
                    FollowingCount = currentUserblogInformation.Item4,
                });
            }
        }
        private void LoadBlogsInformation(string blogName)
        {
            SearchBlogsInformation = new ObservableCollection<BlogInformation>();
            List<Tuple<byte[], string, int, int>> searchBlogsInformation = BlogLogic.GetBlogsInformationByName(blogName);
            foreach (Tuple<byte[], string, int, int> searchBlogInformation in searchBlogsInformation)
            {
                SearchBlogsInformation.Add(new BlogInformation()
                {
                    BlogImage = new BitmapImage(),//ConvertByteArrayToBitMapImage(searchBlogInformation.Item1),
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
                SearchBlogsInformation = new ObservableCollection<BlogInformation>();
            }
            else
            {
                string blogName = BlogName.TextBox.Text;
                BlogName.TextBox.Text = "";
                LoadBlogsInformation(blogName);
            }
        }
        private void BlogButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void OnBlog_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            BlogInformation blogInformation = (sender as ListBox).SelectedItem as BlogInformation;
            BlogContent blogContent = new BlogContent() 
            {
                BlogName = blogInformation.BlogName,
                BlogImage = blogInformation.BlogImage,
                BlogTotalFollowers = blogInformation.FollowingCount,
                BlogTotalPosts = blogInformation.PostCount,
            };
        }
    }
}
