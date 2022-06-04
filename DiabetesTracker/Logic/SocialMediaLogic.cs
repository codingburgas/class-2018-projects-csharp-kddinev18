using DiabetesTracker.Models;
using Microsoft.Win32;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DiabetesTracker.Logic
{
    public static class SocialMediaLogic
    {
        public static BitmapImage ImagePickerDialog(ref string selectedImagePath)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                selectedImagePath = dlg.FileName;
                return new BitmapImage(new Uri(selectedImagePath));
            }
            else
                return null;
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
        public static void LoadBlogsInformation(ObservableCollection<CurrentBlogInformation> YourBlogsInformation)
        {
            List<BlogInformation> currentUserblogsInformation = Services.GetBlogs(CurrentUserInformation.CurrentUserId.Value);
            foreach (BlogInformation currentUserblogInformation in currentUserblogsInformation)
            {
                YourBlogsInformation.Add(new CurrentBlogInformation()
                {
                    BlogId = currentUserblogInformation.BlogId,
                    BlogImage = ConvertByteArrayToBitMapImage(currentUserblogInformation.BlogImage),
                    BlogName = currentUserblogInformation.BlogName,
                    PostCount = currentUserblogInformation.PostCount,
                    FollowingCount = currentUserblogInformation.FollowingCount,
                    IsFollowed = currentUserblogInformation.IsFollowed,
                });
            }
        }
        public static void LoadBlogsInformation(string blogName, ObservableCollection<CurrentBlogInformation> SearchBlogsInformation)
        {
            SearchBlogsInformation.Clear();
            List<BlogInformation> searchBlogsInformation = Services.GetBlogs(CurrentUserInformation.CurrentUserId.Value, blogName);
            foreach (BlogInformation searchBlogInformation in searchBlogsInformation)
            {
                SearchBlogsInformation.Add(new CurrentBlogInformation()
                {
                    BlogId = searchBlogInformation.BlogId,
                    BlogImage = ConvertByteArrayToBitMapImage(searchBlogInformation.BlogImage),
                    BlogName = searchBlogInformation.BlogName,
                    PostCount = searchBlogInformation.PostCount,
                    FollowingCount = searchBlogInformation.FollowingCount,
                    IsFollowed = searchBlogInformation.IsFollowed,
                });
            }
        }
        public static void LoadFollowedBlogsInformation(ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation)
        {
            List<BlogInformation> followingblogsInformation = Services.GetFollowingBlogs(CurrentUserInformation.CurrentUserId.Value);
            foreach (BlogInformation followingblogInformation in followingblogsInformation)
            {
                FollowingBlogsInformation.Add(new CurrentBlogInformation()
                {
                    BlogId = followingblogInformation.BlogId,
                    BlogImage = ConvertByteArrayToBitMapImage(followingblogInformation.BlogImage),
                    BlogName = followingblogInformation.BlogName,
                    PostCount = followingblogInformation.PostCount,
                    FollowingCount = followingblogInformation.FollowingCount,
                    IsFollowed = followingblogInformation.IsFollowed,
                });
            }
        }

        public static (SolidColorBrush, SolidColorBrush) SetPost(ref List<PostInformation> postsInformation, ref BlogContent blogContent, int index, int pagingCount)
        {
            (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) iconColors;
            if (postsInformation[index % pagingCount].IsPostLiked == true)
            {
                iconColors.likeIconColor = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            else
            {
                iconColors.likeIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            if (postsInformation[index % pagingCount].IsPostFavourited == true)
            {
                iconColors.favouriteIconColor = new SolidColorBrush(Colors.Red);
            }
            else
            {
                iconColors.favouriteIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            blogContent.PostContent = postsInformation[index % pagingCount].PostContent;
            blogContent.PostImage = ConvertByteArrayToBitMapImage(postsInformation[index % pagingCount].PostImage);

            return iconColors;
        }
        public static (SolidColorBrush, SolidColorBrush) SetPost(ref List<PostInformation> postsInformation, ref CurrentPostInformation postInformation, int index, int pagingCount)
        {
            (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) iconColors;

            if (postsInformation[index % pagingCount].IsPostLiked == true)
            {
                iconColors.likeIconColor = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            else
            {
                iconColors.likeIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            if (postsInformation[index % pagingCount].IsPostFavourited == true)
            {
                iconColors.favouriteIconColor = new SolidColorBrush(Colors.Red);
            }
            else
            {
                iconColors.favouriteIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            postInformation.BlogName = postsInformation[index % pagingCount].BlogName;
            postInformation.PostContent = postsInformation[index % pagingCount].PostContent;
            postInformation.PostImage = ConvertByteArrayToBitMapImage(postsInformation[index % pagingCount].PostImage);

            return iconColors;

        }
        public static SolidColorBrush LikePost(ref List<PostInformation> postsInformation, int index, int pagingCount)
        {
            if (postsInformation[index % pagingCount].IsPostLiked == false)
            {
                Services.Like(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                postsInformation[index % pagingCount].IsPostLiked = true;
                return new SolidColorBrush(Colors.DeepSkyBlue);
            }
            else
            {
                Services.Unlike(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                postsInformation[index % pagingCount].IsPostLiked = false;
                return (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }
        }

        public static SolidColorBrush FavouritePost(ref List<PostInformation> postsInformation, int index, int pagingCount)
        {
            if (postsInformation[index % pagingCount].IsPostFavourited == false)
            {
                Services.Favourite(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                postsInformation[index % pagingCount].IsPostFavourited = true;
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                Services.Unfavourite(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                postsInformation[index % pagingCount].IsPostFavourited = false;
                return (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }
        }

        public static void FollowBlog(ref BlogContent blogContent, int blogId)
        {
            if (blogContent.IsFollowed == false)
            {
                Services.Follow(CurrentUserInformation.CurrentUserId.Value, blogId);
                blogContent.IsFollowed = true;
            }
            else
            {
                Services.Unfollow(CurrentUserInformation.CurrentUserId.Value, blogId);
                blogContent.IsFollowed = false;
            }
        }

        public static void LoadBlogsInformation(ref ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation)
        {
            List<BlogInformation> followingblogsInformation = Services.GetFollowingBlogs(CurrentUserInformation.CurrentUserId.Value);
            foreach (BlogInformation followingblogInformation in followingblogsInformation)
            {
                FollowingBlogsInformation.Add(new CurrentBlogInformation()
                {
                    BlogId = followingblogInformation.BlogId,
                    BlogImage = ConvertByteArrayToBitMapImage(followingblogInformation.BlogImage),
                    BlogName = followingblogInformation.BlogName,
                    PostCount = followingblogInformation.PostCount,
                    FollowingCount = followingblogInformation.FollowingCount,
                    IsFollowed = followingblogInformation.IsFollowed,
                });
            }
        }

        public static void LoadTagsInformation(ObservableCollection<CurrentTagInformation> TagsInformation)
        {
            List<TagInformation> tagsInformation = Services.GetTags();
            foreach (TagInformation tagInformation in tagsInformation)
            {
                TagsInformation.Add(new CurrentTagInformation()
                {
                    TagId = tagInformation.TagId,
                    TagName = tagInformation.TagName,
                    PostCount = tagInformation.PostCount,
                });
            }
        }

        public static void LoadComments(ObservableCollection<CurrentCommentInformation> Comments, int postId)
        {
            foreach (string comment in Services.GetComments(CurrentUserInformation.CurrentUserId.Value, postId))
            {
                Comments.Add(new CurrentCommentInformation()
                {
                    Comment = comment
                });
            }
        }

        public static void Comment(string comment, int postId)
        {
            if (!HandleUserInput.GeneralHandler(comment))
                return;

            Services.Comment(CurrentUserInformation.CurrentUserId.Value, postId, comment);
        }
    }
}
