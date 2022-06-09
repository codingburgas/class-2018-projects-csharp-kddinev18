using DiabetesTracker.Logic;
using DiabetesTracker.Models;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for TagPostsPage.xaml
    /// </summary>
    public partial class TagPostsPage : Page
    {
        private CurrentPostInformation _postInformation;
        private (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) _iconColors;
        private List<PostInformation> _postsInformation;
        private SocialMediaPage _socialMediaPage;
        private int _index = 0;
        private int _pagingCount = 10;
        private int _tagId;
        public TagPostsPage(int tagId, SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            _tagId = tagId;
            _postInformation = new CurrentPostInformation();
            InitializeComponent();
            DataContext = _postInformation;
            try
            {
                _postsInformation = Services.GetBlogPostsByTag(CurrentUserInformation.CurrentUserId.Value, tagId, 0);
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
            if (_index % _pagingCount == 0)
            {
                _postsInformation = Services.GetBlogPostsByTag(CurrentUserInformation.CurrentUserId.Value, _tagId, _index - 10);
            }
            NextButton.IsEnabled = true;
            _index--;
            SetPost();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_index % _pagingCount == _postsInformation.Count - 1)
            {
                try
                {
                    _postsInformation = Services.GetBlogPostsByTag(CurrentUserInformation.CurrentUserId.Value, _tagId, _index + 1);
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
