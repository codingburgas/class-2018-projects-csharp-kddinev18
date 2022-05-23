using BusinessLogicLayer;
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

namespace DiabetesTracker.ViewModels
{
    public partial class PostsPage : Page
    {
        private CurrentPostInformation _postInformation;

        private List<PostInformation> _postsInformation;
        private int _index = 0;
        private int _pagingCount = 10;

        public PostsPage()
        {
            _postInformation = new CurrentPostInformation();
            _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, 0);
            InitializeComponent();
            this.DataContext = _postInformation;
            SetPost(_index);
        }
        private void SetPost(int index)
        {
            if(_postsInformation[_index % _pagingCount].IsPostLiked == true)
            {
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            else
            {
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            if (_postsInformation[_index % _pagingCount].IsPostFavourited == true)
            {
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            _postInformation.BlogName = _postsInformation[index % _pagingCount].BlogName;
            _postInformation.PostContent = _postsInformation[index % _pagingCount].PostContent;
            _postInformation.PostImage = ConvertByteArrayToBitMapImage(_postsInformation[index % _pagingCount].PostImage);
        }

        public static BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }
            return img;
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
            _index--;
            SetPost(_index % _pagingCount);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(_index % _pagingCount == _postsInformation.Count-1)
            {
                try
                {
                    _postsInformation = Services.GetPosts(CurrentUserInformation.CurrentUserId.Value, _index + 1);
                }
                catch (ArgumentNullException)
                {
                    return;
                }
            }
            _index++;
            SetPost(_index % _pagingCount);
        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_postsInformation[_index].IsPostLiked == false)
            {
                PostLikeLogic.Like(_postsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
                _postsInformation[_index].IsPostLiked = true;
            }
            else
            {
                PostLikeLogic.Unlike(_postsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _postsInformation[_index].IsPostLiked = false;
            }
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_postsInformation[_index].IsPostFavourited == false)
            {
                FavouritePostLogic.FavouritePost(_postsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
                _postsInformation[_index].IsPostFavourited = true;
            }
            else
            {
                FavouritePostLogic.UnfavouritePost(_postsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _postsInformation[_index].IsPostFavourited = false;
            }
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
