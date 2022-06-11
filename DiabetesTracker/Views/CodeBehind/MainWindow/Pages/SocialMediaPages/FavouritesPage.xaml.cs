using DiabetesTracker.Logic;
using DiabetesTracker.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
#nullable disable

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for FavouritesPage.xaml
    /// </summary>
    public partial class FavouritesPage : Page
    {
        // Data binding property for all the favourited posts
        private List<PostInformation> _favouritePostsInformation;
        // Data binding property for the current favourited post
        private CurrentPostInformation _favouritePostInformation = new CurrentPostInformation();
        // Tuple for stoing the current post like icon and favourite icon color
        private (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) _iconColors;
        private SocialMediaPage _socialMediaPage;

        // Index of the current post
        private int _index = 0;

        private int _pagingCount = 10;

        public FavouritesPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            InitializeComponent();

            // Set the DataContext to the data binding property _favouritePostInformation
            DataContext = _favouritePostInformation;
            try
            {
                // Load favourited post information into the data binding property _favouritePostInformation
                _favouritePostsInformation = Services.GetFavouritedPosts(CurrentUserInformation.CurrentUserId.Value, _index);
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
        }
        private void SetPost()
        {
            // Load icon colors
            _iconColors = SocialMediaLogic.SetPost(ref _favouritePostsInformation, ref _favouritePostInformation, _index, _pagingCount);
            // Set the like icon color
            LikeIcon.Foreground = _iconColors.likeIconColor;
            // Set the favourite icon color
            FavouriteIcon.Foreground = _iconColors.favouriteIconColor;
        }
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            // If the index is 0 stop the handler
            if (_index <= 0)
            {
                return;
            }
            // if the current posts are over load previous favourited posts
            if (_index % _pagingCount == 0)
            {
                _favouritePostsInformation = Services.GetFavouritedPosts(CurrentUserInformation.CurrentUserId.Value, _index - 10);
            }
            // Enable NextButton
            NextButton.IsEnabled = true;
            // Decrease the index and set the post
            _index--;
            SetPost();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // if the current favourited posts are over load more posts
            if (_index % _pagingCount == _favouritePostsInformation.Count - 1)
            {
                try
                {
                    // loads the data binding property _favouritePostsInformation with favourited posts
                    _favouritePostsInformation = Services.GetFavouritedPosts(CurrentUserInformation.CurrentUserId.Value, _index + 1);
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
            LikeIcon.Foreground = SocialMediaLogic.LikePost(ref _favouritePostsInformation, _index, _pagingCount);
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            // Like the posts and set the color of the favourite button
            FavouriteIcon.Foreground = SocialMediaLogic.FavouritePost(ref _favouritePostsInformation, _index, _pagingCount);
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            // Shows CommentPage
            _socialMediaPage.ShowPage(new CommentPage(_favouritePostsInformation[_index % _pagingCount].PostId));
        }
    }
}
