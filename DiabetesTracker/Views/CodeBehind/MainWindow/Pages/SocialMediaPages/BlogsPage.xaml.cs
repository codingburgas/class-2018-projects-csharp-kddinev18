using DiabetesTracker.Logic;
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

#nullable disable

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for BlogsPage.xaml
    /// </summary>
    public partial class BlogsPage : Page
    {
        // Data binding property for your blogs information
        public ObservableCollection<CurrentBlogInformation> YourBlogsInformation { get; set; } = new ObservableCollection<CurrentBlogInformation>();
        // Data binding property for the serached blogs information
        public ObservableCollection<CurrentBlogInformation> SearchBlogsInformation { get; set; } = new ObservableCollection<CurrentBlogInformation>();
        // Data binding property for saving the selected property
        public CurrentBlogInformation SelectedBlogInformation { get; set; } = new CurrentBlogInformation();

        private SocialMediaPage _socialMediaPage;
        public BlogsPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            // Load the YourBlogsInformation data binding property with blogs information beloging to the current user
            SocialMediaLogic.LoadBlogsInformation(YourBlogsInformation);
            InitializeComponent();

            // Set the DataContext to this class
            DataContext = this;
        }

        //Event handlers
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Checks user input
            if (!HandleUserInput.GeneralHandler(BlogName.TextBox.Text))
                return;
            else
            {
                // Gets the blog name from BlogName text box
                string blogName = BlogName.TextBox.Text;
                // Resets the text box content
                BlogName.TextBox.Text = "";
                // Loads the serched blog information into the data binding property SearchBlogsInformation
                SocialMediaLogic.LoadBlogsInformation(blogName, SearchBlogsInformation);
            }
        }
        private void OnBlog_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Gets the selected item
            CurrentBlogInformation blogInformation = (sender as ListBox).SelectedItem as CurrentBlogInformation;

            // Shows BlogTemplatePage
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
            // Shows BlogCreationPage
            _socialMediaPage.ShowPage(new BlogCreationPage(_socialMediaPage));
        }
    }
}
