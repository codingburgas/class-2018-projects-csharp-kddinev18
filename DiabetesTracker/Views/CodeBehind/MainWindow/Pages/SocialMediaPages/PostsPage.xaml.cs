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
        private PostInformation _postInformation;

        private List<Tuple<int, string, string, byte[], bool, bool>> _posts;
        private int _index = 0;

        private bool isLiked = false;
        private bool isFavourited = false;

        public PostsPage()
        {
            _postInformation = new PostInformation();
            _posts = PostLogic.GetPosts(CurrentUser.CurrentUserId.Value, 0);
            InitializeComponent();
            this.DataContext = _postInformation;
            SetPost(_index);
        }
        private void SetPost(int index)
        {
            if(_posts[_index].Item5 == true)
            {
                LikeButton.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                LikeButton.Background = new SolidColorBrush(Colors.LightGray);
            }

            if (_posts[_index].Item6 == true)
            {
                FavouriteButton.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                FavouriteButton.Background = new SolidColorBrush(Colors.LightGray);
            }

            _postInformation.BlogName = _posts.ElementAt(index).Item2;
            _postInformation.PostContent = _posts.ElementAt(index).Item3;
            _postInformation.PostImage = ConvertByteArrayToBitMapImage(_posts.ElementAt(index).Item4);
        }

        private static BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
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
            if(_index % 10 == 0)
            {
                _posts = PostLogic.GetPosts(CurrentUser.CurrentUserId.Value ,_index - 10);
            }
            _index--;
            SetPost(_index % 10);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(_index % 10 == _posts.Count-1)
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
            SetPost(_index % 10);
        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_posts[_index].Item5 == false)
            {
                PostLikeLogic.Like(_posts[_index % 10].Item1, CurrentUser.CurrentUserId.Value);
            }
            else
            {
                PostLikeLogic.Unlike(_posts[_index % 10].Item1, CurrentUser.CurrentUserId.Value);
            }
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_posts[_index].Item6 == false)
            {
                PostLikeLogic.Like(_posts[_index % 10].Item1, CurrentUser.CurrentUserId.Value);
            }
            else
            {
                PostLikeLogic.Unlike(_posts[_index % 10].Item1, CurrentUser.CurrentUserId.Value);
            }
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
