using DiabetesTracker.Logic;
using DiabetesTracker.Models;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class PostsPage : Page
    {
        private SocialMediaPage _socialMediaPage;
        // Tuple for stoing the current post like icon and favourite icon color
        private (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) _iconColors;
        // Data binding property for the current post
        private CurrentPostInformation _postInformation;
        // Data binding property for the posts
        private List<PostInformation> _postsInformation;

        private int _index = 0;
        private int _pagingCount = 10;

        public PostsPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;

            _postInformation = new CurrentPostInformation();
            InitializeComponent();

            // Set the DataContext to the data binding property _postInformation
            DataContext = _postInformation;

            try
            {
                // Load the post information into the data binding property _postInformation
                _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, _index);
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
            _iconColors = SocialMediaLogic.SetPost(ref _postsInformation, ref _postInformation, _index, _pagingCount);
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
                _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, _index - 10);
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
            if(_index % _pagingCount == _postsInformation.Count-1)
            {
                try
                {
                    // loads the data binding property _postsInformation with posts
                    _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, _index + 1);
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
            LikeIcon.Foreground = SocialMediaLogic.LikePost(ref _postsInformation, _index, _pagingCount);
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            // Like the posts and set the color of the favourite button
            FavouriteIcon.Foreground = SocialMediaLogic.FavouritePost(ref _postsInformation, _index, _pagingCount);
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            // Shows CommentPage
            _socialMediaPage.ShowPage(new CommentPage(_postsInformation[_index % _pagingCount].PostId));
        }
    }
}
