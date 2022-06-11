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
#nullable disable

namespace DiabetesTracker.Logic
{
    public static class SocialMediaLogic
    {
        // Opens a file dopblog box and sets the selected item path
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

        // Converts a byte array into a BitmapImage
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

        // Load information into ObservableCollection<CurrentBlogInformation> YourBlogsInformation
        public static void LoadBlogsInformation(ObservableCollection<CurrentBlogInformation> YourBlogsInformation)
        {
            // Get blogs belonging to the current user
            List<BlogInformation> currentUserblogsInformation = Services.GetBlogs(CurrentUserInformation.CurrentUserId.Value);

            // For every blog in currentUserblogsInformation add CurrentBlogInformation in ObservableCollection<CurrentBlogInformation> YourBlogsInformation
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

        // Load information into ObservableCollection<CurrentBlogInformation> SearchBlogsInformation
        public static void LoadBlogsInformation(string blogName, ObservableCollection<CurrentBlogInformation> SearchBlogsInformation)
        {
            // Clear the list
            SearchBlogsInformation.Clear();

            // Get blogs by name
            List<BlogInformation> searchBlogsInformation = Services.GetBlogs(CurrentUserInformation.CurrentUserId.Value, blogName);
            // For every blog in searchBlogsInformation add CurrentBlogInformation in ObservableCollection<CurrentBlogInformation> SearchBlogsInformation
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

        // Load information into ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation
        public static void LoadFollowedBlogsInformation(ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation)
        {
            // Get blogs followed by the current user
            List<BlogInformation> followingblogsInformation = Services.GetFollowingBlogs(CurrentUserInformation.CurrentUserId.Value);
            // For every blog in followingblogsInformation add CurrentBlogInformation in ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation
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

        // Sets the next or previous post in a blog and returns the colors of the like and favourite button
        public static (SolidColorBrush, SolidColorBrush) SetPost(ref List<PostInformation> postsInformation, ref BlogContent blogContent, int index, int pagingCount)
        {
            (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) iconColors;

            // If the next post is liked set the likeIconColor to DeepSkyBlue
            if (postsInformation[index % pagingCount].IsPostLiked == true)
            {
                iconColors.likeIconColor = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            // Else set the likeIconColor to #2b2b2b
            else
            {
                iconColors.likeIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            // If the next post is favourited set the favouriteIconColor to Red
            if (postsInformation[index % pagingCount].IsPostFavourited == true)
            {
                iconColors.favouriteIconColor = new SolidColorBrush(Colors.Red);
            }
            // Else set the favouriteIconColor to #2b2b2b
            else
            {
                iconColors.favouriteIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            // Set the content of the post to the next post's content
            blogContent.PostContent = postsInformation[index % pagingCount].PostContent;
            // Set the image of the post to the next post's image
            blogContent.PostImage = ConvertByteArrayToBitMapImage(postsInformation[index % pagingCount].PostImage);

            // Returns the colors of the likeIcon and favouriteIcon
            return iconColors;
        }

        // Sets the next or previous post and returns the colors of the like and favourite button
        public static (SolidColorBrush, SolidColorBrush) SetPost(ref List<PostInformation> postsInformation, ref CurrentPostInformation postInformation, int index, int pagingCount)
        {
            (SolidColorBrush likeIconColor, SolidColorBrush favouriteIconColor) iconColors;

            // If the next post is liked set the likeIconColor to DeepSkyBlue
            if (postsInformation[index % pagingCount].IsPostLiked == true)
            {
                iconColors.likeIconColor = new SolidColorBrush(Colors.DeepSkyBlue);
            }
            // Else set the likeIconColor to #2b2b2b
            else
            {
                iconColors.likeIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            // If the next post is favourited set the favouriteIconColor to Red
            if (postsInformation[index % pagingCount].IsPostFavourited == true)
            {
                iconColors.favouriteIconColor = new SolidColorBrush(Colors.Red);
            }
            // Else set the favouriteIconColor to #2b2b2b
            else
            {
                iconColors.favouriteIconColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }

            // Set the blog name of the post to the next post's blog name
            postInformation.BlogName = postsInformation[index % pagingCount].BlogName;
            // Set the content of the post to the next post's content
            postInformation.PostContent = postsInformation[index % pagingCount].PostContent;
            // Set the image of the post to the next post's image
            postInformation.PostImage = ConvertByteArrayToBitMapImage(postsInformation[index % pagingCount].PostImage);

            // Returns the colors of the likeIcon and favouriteIcon
            return iconColors;

        }

        // Likes or unlikes a specific post and returns the color of the like icon
        public static SolidColorBrush LikePost(ref List<PostInformation> postsInformation, int index, int pagingCount)
        {
            // If the post is not liked, like it
            if (postsInformation[index % pagingCount].IsPostLiked == false)
            {
                // Like the specific post
                Services.Like(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                // Set the like state of the post to liked
                postsInformation[index % pagingCount].IsPostLiked = true;
                // Return color DeepSkyBlue
                return new SolidColorBrush(Colors.DeepSkyBlue);
            }
            // If the post is liked, unlike it
            else
            {
                // Unlike the specific post
                Services.Unlike(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                // Set the like state of the post to unliked
                postsInformation[index % pagingCount].IsPostLiked = false;
                // Return color DeepSkyBlue
                return (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }
        }

        // Favourites or unfavourites a specific post and returns the color of the favourite icon
        public static SolidColorBrush FavouritePost(ref List<PostInformation> postsInformation, int index, int pagingCount)
        {
            // If the post is not favourited, favourite it
            if (postsInformation[index % pagingCount].IsPostFavourited == false)
            {
                // Favourites the specific post
                Services.Favourite(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                // Set the favourite state of the post to favourited
                postsInformation[index % pagingCount].IsPostFavourited = true;
                // Return color DeepSkyBlue
                return new SolidColorBrush(Colors.Red);
            }
            // If the post is favourited, unfavourite it
            else
            {
                // Unfavourites the specific post
                Services.Unfavourite(postsInformation[index % pagingCount].PostId, CurrentUserInformation.CurrentUserId.Value);
                // Set the favourite state of the post to unfavourited
                postsInformation[index % pagingCount].IsPostFavourited = false;
                // Return color DeepSkyBlue
                return (SolidColorBrush)new BrushConverter().ConvertFrom("#2b2b2b");
            }
        }

        // Follow or unfollow
        public static void FollowBlog(ref BlogContent blogContent, int blogId)
        {
            // If the blog is not followed, follow it
            if (blogContent.IsFollowed == false)
            {
                // Follow the specific blog
                Services.Follow(CurrentUserInformation.CurrentUserId.Value, blogId);
                // Set the follow state od the blog to followed
                blogContent.IsFollowed = true;
            }
            // Else unfollow it
            else
            {
                // Unfollow the specific blog
                Services.Unfollow(CurrentUserInformation.CurrentUserId.Value, blogId);
                // Set the follow state od the blog to unfollowed
                blogContent.IsFollowed = false;
            }
        }

        // Load blogs information followed by the current user
        public static void LoadBlogsInformation(ref ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation)
        {
            // Get blogs followed by the curent user
            List<BlogInformation> followingblogsInformation = Services.GetFollowingBlogs(CurrentUserInformation.CurrentUserId.Value);

            // Arrange the blogs into a list ObservableCollection<CurrentBlogInformation> FollowingBlogsInformation 
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

        // Load tags information
        public static void LoadTagsInformation(ObservableCollection<CurrentTagInformation> TagsInformation)
        {
            // Get tags
            List<TagInformation> tagsInformation = Services.GetTags();

            // Arrange the tags into a list ObservableCollection<CurrentBlogInformation> TagsInformation 
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

        // Load comments information
        public static void LoadComments(ObservableCollection<CurrentCommentInformation> Comments, int postId)
        {
            // Arrange the comments
            foreach (string comment in Services.GetComments(CurrentUserInformation.CurrentUserId.Value, postId))
            {
                Comments.Add(new CurrentCommentInformation()
                {
                    Comment = comment
                });
            }
        }

        // Add a comment
        public static void Comment(string comment, int postId)
        {
            // Check the comment
            if (!HandleUserInput.GeneralHandler(comment))
                return;

            // Add comment
            Services.Comment(CurrentUserInformation.CurrentUserId.Value, postId, comment);
        }
    }
}
