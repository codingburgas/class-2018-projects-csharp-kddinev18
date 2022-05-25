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

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for FavouritesPage.xaml
    /// </summary>
    public partial class FavouritesPage : Page
    {
        private List<PostInformation> _favouritePostsInformation;
        private CurrentPostInformation _favouritePostInformation = new CurrentPostInformation();
        private int _index = 0;
        private int _pagingCount = 10;

        public FavouritesPage()
        {
            InitializeComponent();
            DataContext = _favouritePostInformation;
            try
            {
                _favouritePostsInformation = Services.GetFavouritedPosts(CurrentUserInformation.CurrentUserId.Value, 0);
                SetPost(_index);
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

        private void SetPost(int index)
        {
            if (_favouritePostsInformation[_index % _pagingCount].IsPostLiked == true)
            {
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            else
            {
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            if (_favouritePostsInformation[_index % _pagingCount].IsPostFavourited == true)
            {
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            _favouritePostInformation.BlogName = _favouritePostsInformation[index % _pagingCount].BlogName;
            _favouritePostInformation.PostContent = _favouritePostsInformation[index % _pagingCount].PostContent;
            _favouritePostInformation.PostImage = PostsPage.ConvertByteArrayToBitMapImage(_favouritePostsInformation[index % _pagingCount].PostImage);
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
            _index--;
            SetPost(_index % _pagingCount);
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
                    return;
                }
            }
            _index++;
            SetPost(_index % _pagingCount);
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_favouritePostsInformation[_index % _pagingCount].IsPostLiked == false)
            {
                Services.Like(_favouritePostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
                _favouritePostsInformation[_index % _pagingCount].IsPostLiked = true;
            }
            else
            {
                Services.Unlike(_favouritePostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _favouritePostsInformation[_index % _pagingCount].IsPostLiked = false;
            }
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_favouritePostsInformation[_index % _pagingCount].IsPostFavourited == false)
            {
                Services.Favourite(_favouritePostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
                _favouritePostsInformation[_index % _pagingCount].IsPostFavourited = true;
            }
            else
            {
                Services.Unfavourite(_favouritePostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _favouritePostsInformation[_index % _pagingCount].IsPostFavourited = false;
            }
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
