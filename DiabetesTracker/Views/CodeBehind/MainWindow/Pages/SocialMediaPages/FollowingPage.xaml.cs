using DiabetesTracker.Logic;
using DiabetesTracker.Models;
using ServiceLayer;
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
#nullable disable

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for FollowingPage.xaml
    /// </summary>
    public partial class FollowingPage : Page
    {
        // Data binding property for the followed blogs information
        public ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation { get; set; } = new ObservableCollection<CurrentBlogInformation>();
        // Data binding property for the selected blog information
        public CurrentBlogInformation SelectedBlogInformation { get; set; } = new CurrentBlogInformation();

        private SocialMediaPage _socialMediaPage;
        public FollowingPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            // Load followed blog information into the data binding property FollowingBlogsInformation
            SocialMediaLogic.LoadFollowedBlogsInformation(FollowingBlogsInformation);
            InitializeComponent();
            // Set the DataContext to this class
            DataContext = this;
        }

        private void OnBlog_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Get the selected blog
            CurrentBlogInformation blogInformation = (sender as ListBox).SelectedItem as CurrentBlogInformation;
            // Show BlogTemplatePage
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
                blogInformation.BlogId)
            );
        }
    }
}
