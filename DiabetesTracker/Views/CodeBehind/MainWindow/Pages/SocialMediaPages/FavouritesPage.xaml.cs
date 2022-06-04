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

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for FavouritesPage.xaml
    /// </summary>
    public partial class FavouritesPage : Page
    {
        private List<PostInformation> _favouritePostsInformation;
        private CurrentPostInformation _favouritePostInformation = new CurrentPostInformation();
        private (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) _iconColors;
        private SocialMediaPage _socialMediaPage;

        private int _index = 0;
        private int _pagingCount = 10;

        public FavouritesPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            InitializeComponent();
            DataContext = _favouritePostInformation;
            try
            {
                _favouritePostsInformation = Services.GetFavouritedPosts(CurrentUserInformation.CurrentUserId.Value, _index);
                SetPost();
            }
            catch (Exception)
            {
                PrevButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                LikeButton.IsEnabled = false;
                FavouriteButton.IsEnabled = false;
                CommentButton.IsEnabled = false;
            }
        }
        private void SetPost()
        {
            _iconColors = SocialMediaLogic.SetPost(ref _favouritePostsInformation, ref _favouritePostInformation, _index, _pagingCount);
            LikeIcon.Foreground = _iconColors.likeIconColor;
            FavouriteIcon.Foreground = _iconColors.favouriteIconColor;
        }
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_index <= 0)
            {
                return;
            }
            if (_index % _pagingCount == 0)
            {
                _favouritePostsInformation = Services.GetFavouritedPosts(CurrentUserInformation.CurrentUserId.Value, _index - 10);
            }
            NextButton.IsEnabled = true;
            _index--;
            SetPost();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_index % _pagingCount == _favouritePostsInformation.Count - 1)
            {
                try
                {
                    _favouritePostsInformation = Services.GetFavouritedPosts(CurrentUserInformation.CurrentUserId.Value, _index + 1);
                }
                catch (Exception)
                {
                    NextButton.IsEnabled = false;
                    return;
                }
            }
            _index++;
            SetPost();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            LikeIcon.Foreground = SocialMediaLogic.LikePost(ref _favouritePostsInformation, _index, _pagingCount);
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            FavouriteIcon.Foreground = SocialMediaLogic.FavouritePost(ref _favouritePostsInformation, _index, _pagingCount);
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            _socialMediaPage.ShowPage(new CommentPage(_favouritePostsInformation[_index % _pagingCount].PostId));
        }
    }
}
