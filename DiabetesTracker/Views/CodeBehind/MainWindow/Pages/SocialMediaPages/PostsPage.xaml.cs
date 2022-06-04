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

namespace DiabetesTracker.Views
{
    public partial class PostsPage : Page
    {
        private SocialMediaPage _socialMediaPage;
        private (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) _iconColors;
        private CurrentPostInformation _postInformation;
        private List<PostInformation> _postsInformation;

        private int _index = 0;
        private int _pagingCount = 10;

        public PostsPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;

            _postInformation = new CurrentPostInformation();
            InitializeComponent();
            DataContext = _postInformation;
            try
            {
                _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, _index);
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
            _iconColors = SocialMediaLogic.SetPost(ref _postsInformation, ref _postInformation, _index, _pagingCount);
            LikeIcon.Foreground = _iconColors.likeIconColor;
            FavouriteIcon.Foreground = _iconColors.favouriteIconColor;
        }


        //Event handlers
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_index <= 0)
            {
                return;
            }
            if(_index % _pagingCount == 0)
            {
                _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, _index - 10);
            }
            NextButton.IsEnabled = true;
            _index--;
            SetPost();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(_index % _pagingCount == _postsInformation.Count-1)
            {
                try
                {
                    _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, _index + 1);
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
            LikeIcon.Foreground = SocialMediaLogic.LikePost(ref _postsInformation, _index, _pagingCount);
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            FavouriteIcon.Foreground = SocialMediaLogic.FavouritePost(ref _postsInformation, _index, _pagingCount);
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            _socialMediaPage.ShowPage(new CommentPage(_postsInformation[_index % _pagingCount].PostId));
        }
    }
}
