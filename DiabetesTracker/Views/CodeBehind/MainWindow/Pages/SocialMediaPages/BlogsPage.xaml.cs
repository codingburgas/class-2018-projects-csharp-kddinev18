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
            _socialMediaPage.ShowPage(new BlogTemplatePage(
                new BlogContent()
                {
                    BlogName = blogInformation.BlogName,
                    BlogImage = blogInformation.BlogImage,
                    BlogTotalFollowers = blogInformation.FollowingCount,
                    BlogTotalPosts = blogInformation.PostCount,
                    BelongsToUser = Services.BelongsToUser(CurrentUserInformation.CurrentUserId.Value, blogInformation.BlogId),
                    IsFollowed = blogInformation.IsFollowed
                },
                _socialMediaPage, 
                blogInformation.BlogId
            ));
        }
        private void CreateBlogButton_Click(object sender, RoutedEventArgs e)
        {
            _socialMediaPage.ShowPage(new BlogCreationPage(_socialMediaPage));
        }
    }
}
