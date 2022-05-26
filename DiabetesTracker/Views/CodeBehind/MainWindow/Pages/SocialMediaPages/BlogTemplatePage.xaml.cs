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
using DiabetesTracker.Models;
using ServiceLayer;

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for BlogTemplatePage.xaml
    /// </summary>
    public partial class BlogTemplatePage : Page
    {
        private BlogContent _blogContent;

        private List<PostInformation> _blogPostsInformation;
        private int _index = 0;
        private int _pagingCount = 10;
        private int _blogId;
        public BlogTemplatePage(BlogContent BlogContent, int blogId)
        {
            _blogId = blogId;
            _blogContent = BlogContent;
            InitializeComponent();
            DataContext = _blogContent;
            try
            {
                _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, 0, _blogId);
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

            FollowButton.IsEnabled = !_blogContent.BelongsToUser;
            PostButton.IsEnabled = _blogContent.BelongsToUser;
        }

        private void SetPost(int index)
        {
            if(_blogPostsInformation[_index % _pagingCount].IsPostLiked == true)
            {
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            else
            {
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            if (_blogPostsInformation[_index % _pagingCount].IsPostFavourited == true)
            {
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            _blogContent.PostContent = _blogPostsInformation[index % _pagingCount].PostContent;
            _blogContent.PostImage = PostsPage.ConvertByteArrayToBitMapImage(_blogPostsInformation[index % _pagingCount].PostImage);
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
                _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, _index - 10, _blogId);
            }
            _index--;
            SetPost(_index % _pagingCount);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(_index % _pagingCount == _blogPostsInformation.Count-1)
            {
                try
                {
                    _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, _index + 1, _blogId);
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
            if (_blogPostsInformation[_index % _pagingCount].IsPostLiked == false)
            {
                Services.Like(_blogPostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                LikeIcon.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
                _blogPostsInformation[_index % _pagingCount].IsPostLiked = true;
            }
            else
            {
                Services.Unlike(_blogPostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                LikeIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _blogPostsInformation[_index % _pagingCount].IsPostLiked = false;
            }
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_blogPostsInformation[_index % _pagingCount].IsPostFavourited == false)
            {
                Services.Favourite(_blogPostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                FavouriteIcon.Foreground = new SolidColorBrush(Colors.Red);
                _blogPostsInformation[_index % _pagingCount].IsPostFavourited = true;
            }
            else
            {
                Services.Unfavourite(_blogPostsInformation[_index % _pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                FavouriteIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
                _blogPostsInformation[_index % _pagingCount].IsPostFavourited = false;
            }
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void FollowButton_Click(object sender, RoutedEventArgs e)
        {
            if (_blogContent.IsFollowed == false)
            {
                Services.Follow(CurrentUserInformation.CurrentUserId.Value, _blogId);
                _blogContent.IsFollowed = true;
            }
            else
            {
                Services.Unfollow(CurrentUserInformation.CurrentUserId.Value, _blogId);
                _blogContent.IsFollowed = false;
            }
        }
        private void PostButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
