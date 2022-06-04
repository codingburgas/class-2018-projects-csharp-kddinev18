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
using DiabetesTracker.Logic;
using DiabetesTracker.Models;
using ServiceLayer;

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for BlogTemplatePage.xaml
    /// </summary>
    public partial class BlogTemplatePage : Page
    {
        private BlogContent _blogContent;
        private SocialMediaPage _socialMediaPage;
        private (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) _iconColors;

        private List<PostInformation> _blogPostsInformation;
        private int _index = 0;
        private int _pagingCount = 10;
        private int _blogId;
        public BlogTemplatePage(BlogContent BlogContent, SocialMediaPage socialMediaPage, int blogId)
        {
            _socialMediaPage = socialMediaPage;
            _blogId = blogId;
            _blogContent = BlogContent;
            InitializeComponent();
            DataContext = _blogContent;
            try
            {
                _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, _index, _blogId);
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

            FollowButton.IsEnabled = !_blogContent.BelongsToUser;
            PostButton.IsEnabled = _blogContent.BelongsToUser;
        }
        private void SetPost()
        {
            _iconColors = SocialMediaLogic.SetPost(ref _blogPostsInformation, ref _blogContent, _index, _pagingCount);
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
                _blogPostsInformation = Services.GetBlogPosts(CurrentUserInformation.CurrentUserId.Value, _index - 10, _blogId);
            }
            NextButton.IsEnabled = true;
            _index--;
            SetPost();
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
                    NextButton.IsEnabled = false;
                    return;
                }
            }
            _index++;
            SetPost();
        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            LikeIcon.Foreground = SocialMediaLogic.LikePost(ref _blogPostsInformation, _index, _pagingCount);
        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            FavouriteIcon.Foreground = SocialMediaLogic.FavouritePost(ref _blogPostsInformation, _index, _pagingCount);
        }
        private void FollowButton_Click(object sender, RoutedEventArgs e)
        {
            SocialMediaLogic.FollowBlog(ref _blogContent, _blogId);
        }
        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            _socialMediaPage.ShowPage(new PostPostingPage(_socialMediaPage,_blogId));
        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            _socialMediaPage.ShowPage(new CommentPage(_blogPostsInformation[_index % _pagingCount].PostId));
        }
    }
}
