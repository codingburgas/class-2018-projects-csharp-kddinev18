using BusinessLogicLayer;
using DiabetesTracker.Models;
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

        private List<PostInformation> _posts;
        private int _index = 0;
        private int _pagingCount = 10;

        public PostsPage()
        {
            _postInformation = new CurrentPostInformation();
            _posts = PostLogic.GetPosts(CurrentUser.CurrentUserId.Value, 0);
            InitializeComponent();
            this.DataContext = _postInformation;
            SetPost(_index);
        }
        private void SetPost(int index)
        {
            if(_posts[_index % _pagingCount].IsPostLiked == true)
            {
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            else
            {
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            if (_posts[_index % _pagingCount].IsPostFavourited == true)
            {
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            _postInformation.BlogName = _posts[index % _pagingCount].BlogName;
            _postInformation.PostContent = _posts[index % _pagingCount].PostContent;
            _postInformation.PostImage = ConvertByteArrayToBitMapImage(_posts[index % _pagingCount].PostImage);
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
                _posts = PostLogic.GetPosts(CurrentUser.CurrentUserId.Value ,_index - 10);
            }
            _index--;
            SetPost(_index % _pagingCount);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(_index % _pagingCount == _posts.Count-1)
            {
                try
                {
                    _posts = PostLogic.GetPosts(CurrentUser.CurrentUserId.Value, _index + 1);
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
            if (_posts[_index].IsPostLiked == false)
            {
                PostLikeLogic.Like(_posts[_index % _pagingCount].PostId, CurrentUser.CurrentUserId.Value);
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
                _posts[_index].IsPostLiked = true;
            }
            else
            {
                PostLikeLogic.Unlike(_posts[_index % _pagingCount].PostId, CurrentUser.CurrentUserId.Value);
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _posts[_index].IsPostLiked = false;
            }
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_posts[_index].IsPostFavourited == false)
            {
                FavouritePostLogic.FavouritePost(_posts[_index % _pagingCount].PostId, CurrentUser.CurrentUserId.Value);
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
                _posts[_index].IsPostFavourited = true;
            }
            else
            {
                FavouritePostLogic.UnfavouritePost(_posts[_index % _pagingCount].PostId, CurrentUser.CurrentUserId.Value);
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _posts[_index].IsPostFavourited = false;
            }
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
