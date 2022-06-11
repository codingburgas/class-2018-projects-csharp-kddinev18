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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiabetesTracker.Logic;
using DiabetesTracker.Models;
using ServiceLayer;

#nullable disable

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for BlogTemplatePage.xaml
    /// </summary>
    public partial class BlogTemplatePage : Page
    {
        // Data binding property for blog information
        private BlogContent _blogContent;
        private SocialMediaPage _socialMediaPage;
        // Tuple for stoing the current post like icon and favourite icon color
        private (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) _iconColors;

        // Data binding property for the posts belonging to the current blog
        private List<PostInformation> _blogPostsInformation;

        // Index of the current post
        private int _index = 0;

        private int _pagingCount = 10;
        private int _blogId;
        public BlogTemplatePage(BlogContent BlogContent, SocialMediaPage socialMediaPage, int blogId)
        {
            _socialMediaPage = socialMediaPage;
            _blogId = blogId;
            _blogContent = BlogContent;
            InitializeComponent();

            // Set the data context to _blogContent
            DataContext = _blogContent;
            try
            {
                // loads the data binding property _blogPostsInformation with posts belonging to the current blog
                _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, _index, _blogId);
                SetPost();
            }
            catch (Exception)
            {
                // Diable all button if there are no posts
                PrevButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                LikeButton.IsEnabled = false;
                FavouriteButton.IsEnabled = false;
                CommentButton.IsEnabled = false;
            }

            // Diable the follow button is the blog belongs to the user
            FollowButton.IsEnabled = !_blogContent.BelongsToUser;
            // diable the post button if the blog does not delong to the user
            PostButton.IsEnabled = _blogContent.BelongsToUser;
        }
        private void SetPost()
        {
            // Load icon colors
            _iconColors = SocialMediaLogic.SetPost(ref _blogPostsInformation, ref _blogContent, _index, _pagingCount);
            // Set the like icon color
            LikeIcon.Foreground = _iconColors.likeIconColor;
            // Set the favourite icon color
            FavouriteIcon.Foreground = _iconColors.favouriteIconColor;
        }

        //Event handlers
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            // If the index is 0 stop the handler
            if (_index <= 0)
            {
                return;
            }
            // if the current posts are over load previous posts
            if(_index % _pagingCount == 0)
            {
                _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, _index - 10, _blogId);
            }

            // Enable NextButton
            NextButton.IsEnabled = true;
            
            // Decrease the index and set the post
            _index--;
            SetPost();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // if the current posts are over load more posts
            if (_index % _pagingCount == _blogPostsInformation.Count-1)
            {
                try
                {
                    // loads the data binding property _blogPostsInformation with posts belonging to the current blog
                    _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, _index + 1, _blogId);
                }
                catch (Exception)
                {
                    // Desable the next button when there are no more posts
                    NextButton.IsEnabled = false;
                    return;
                }
            }
            // Increase the index and set the post
            _index++;
            SetPost();
        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            // Like the posts and set the color of the like button
            LikeIcon.Foreground = SocialMediaLogic.LikePost(ref _blogPostsInformation, _index, _pagingCount);
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            // Like the posts and set the color of the favourite button
            FavouriteIcon.Foreground = SocialMediaLogic.FavouritePost(ref _blogPostsInformation, _index, _pagingCount);
        }
        private void FollowButton_Click(object sender, RoutedEventArgs e)
        {
            // Follow the current blog
            SocialMediaLogic.FollowBlog(ref _blogContent, _blogId);
        }
        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            // Shows PostPostingPage
            _socialMediaPage.ShowPage(new PostPostingPage(_socialMediaPage,_blogId));
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            // Shows CommentPage
            _socialMediaPage.ShowPage(new CommentPage(_blogPostsInformation[_index % _pagingCount].PostId));
        }
    }
}
