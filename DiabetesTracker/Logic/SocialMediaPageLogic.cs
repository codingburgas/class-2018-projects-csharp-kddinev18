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
using System.Windows.Media.Imaging;

namespace DiabetesTracker.Logic
{
    public static class SocialMediaPageLogic
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
    }
}
