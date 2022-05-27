using DiabetesTracker.Models;
using ServiceLayer;
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

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for BlogsPage.xaml
    /// </summary>
    public partial class BlogsPage : Page
    {
        public ObservableCollection<CurrentBlogInformation> YourBlogsInformation { get; set; } = new ObservableCollection<CurrentBlogInformation>();
        public ObservableCollection<CurrentBlogInformation> SearchBlogsInformation { get; set; } = new ObservableCollection<CurrentBlogInformation>();
        public CurrentBlogInformation SelectedBlogInformation { get; set; } = new CurrentBlogInformation();

        private SocialMediaPage _socialMediaPage;
        public BlogsPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            LoadBlogsInformation();
            InitializeComponent();
            DataContext = this;
        }
        private void LoadBlogsInformation()
        {
            List<BlogInformation> currentUserblogsInformation = Services.GetBlogs(CurrentUserInformation.CurrentUserId.Value);
            foreach (BlogInformation currentUserblogInformation in currentUserblogsInformation)
            {
                YourBlogsInformation.Add(new CurrentBlogInformation() 
                {
                    BlogId = currentUserblogInformation.BlogId,
                    BlogImage = new BitmapImage(),//ConvertByteArrayToBitMapImage(searchBlogInformation.BlogImage),
                    BlogName = currentUserblogInformation.BlogName,
                    PostCount = currentUserblogInformation.PostCount,
                    FollowingCount = currentUserblogInformation.FollowingCount,
                    IsFollowed = currentUserblogInformation.IsFollowed,
                });
            }
        }
        private void LoadBlogsInformation(string blogName)
        {
            SearchBlogsInformation.Clear();
            List<BlogInformation> searchBlogsInformation = Services.GetBlogs(CurrentUserInformation.CurrentUserId.Value,blogName);
            foreach (BlogInformation searchBlogInformation in searchBlogsInformation)
            {
                SearchBlogsInformation.Add(new CurrentBlogInformation()
                {
                    BlogId = searchBlogInformation.BlogId,
                    BlogImage = new BitmapImage(),//ConvertByteArrayToBitMapImage(searchBlogInformation.BlogImage),
                    BlogName = searchBlogInformation.BlogName,
                    PostCount = searchBlogInformation.PostCount,
                    FollowingCount = searchBlogInformation.FollowingCount,
                    IsFollowed = searchBlogInformation.IsFollowed,
                });
            }
        }

        //Event handlers
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (BlogName.TextBox.Text == "")
            {
                SearchBlogsInformation.Clear();
            }
            else
            {
                string blogName = BlogName.TextBox.Text;
                BlogName.TextBox.Text = "";
                LoadBlogsInformation(blogName);
            }
        }
        private void OnBlog_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            CurrentBlogInformation blogInformation = (sender as ListBox).SelectedItem as CurrentBlogInformation;
            BlogContent blogContent = new BlogContent() 
            {
                BlogName = blogInformation.BlogName,
                BlogImage = blogInformation.BlogImage,
                BlogTotalFollowers = blogInformation.FollowingCount,
                BlogTotalPosts = blogInformation.PostCount,
                BelongsToUser = Services.BelongsToUser(CurrentUserInformation.CurrentUserId.Value, blogInformation.BlogId),
                IsFollowed = blogInformation.IsFollowed
            };
            _socialMediaPage.ShowPage(new BlogTemplatePage(blogContent, blogInformation.BlogId));
        }
    }
}
