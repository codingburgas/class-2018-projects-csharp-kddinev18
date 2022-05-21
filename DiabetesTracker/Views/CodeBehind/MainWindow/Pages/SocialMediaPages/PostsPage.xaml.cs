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

        private List<Tuple<string, string, byte[]>> _posts;
        private int _index = 0;
        public PostsPage()
        {
            _postInformation = new PostInformation();
            _posts = PostBusinessLogic.GetPosts(0);
            InitializeComponent();
            this.DataContext = _postInformation;
            SetPost(_index);
        }
        private void SetPost(int index)
        {
            _postInformation.BlogName = _posts.ElementAt(index).Item1;
            _postInformation.PostContent = _posts.ElementAt(index).Item2;
            _postInformation.PostImage = ConvertByteArrayToBitMapImage(_posts.ElementAt(index).Item3);
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
                _posts = PostBusinessLogic.GetPosts(_index - 10);
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
                    _posts = PostBusinessLogic.GetPosts(_index+1);
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

        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
