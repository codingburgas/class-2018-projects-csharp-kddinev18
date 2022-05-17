using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DiabetesTracker.Models
{
    public static class MainWindowModel
    {
        public static List<Tuple<string,string,BitmapImage>> GetPosts(int skipCount)
        {
            List<int> postIds = PostBusinessLogic.GetPostIds(skipCount);

            List<Tuple<string, string, BitmapImage>> postsInformation = new List<Tuple<string, string, BitmapImage>>();

            foreach (int id in postIds)
            {
                postsInformation.Add(new Tuple<string, string, BitmapImage>(
                    PostBusinessLogic.GetPostBlogName(id), 
                    PostBusinessLogic.GetPostContent(id),
                    ConvertByteArrayToBitMapImage(PostBusinessLogic.GetPostImage(id))
                ));
            }

            return postsInformation;
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
        public static List<Tuple<BitmapImage, string, int, int>> GetCurrentUserBlogsInformation()
        {
            List<int> currentUserBlogsIds = BlogBusinessLogic.GetCurrentUserBlogs();

            List<Tuple<BitmapImage, string, int, int>> currentUserBlogsInformation = new List<Tuple<BitmapImage, string, int, int>>();
            foreach (int id in currentUserBlogsIds)
            {
                currentUserBlogsInformation.Add(new Tuple<BitmapImage, string, int, int>(
                    new BitmapImage(),
                    BlogBusinessLogic.GetBlogName(id),
                    BlogBusinessLogic.GetBlogPostsCount(id),
                    BlogBusinessLogic.GetBlogFollowingCount(id)
                ));
            }
            return currentUserBlogsInformation;
        }

        public static List<Tuple<BitmapImage, string, int, int>> GetBlogsInformationByName(string blogName)
        {
            List<int> blogsIdsByName = BlogBusinessLogic.GetBlogsIdByName(blogName);

            List<Tuple<BitmapImage, string, int, int>> blogsInformationByName = new List<Tuple<BitmapImage, string, int, int>>();
            foreach (int id in blogsIdsByName)
            {
                blogsInformationByName.Add(new Tuple<BitmapImage, string, int, int>(
                    new BitmapImage(),
                    BlogBusinessLogic.GetBlogName(id),
                    BlogBusinessLogic.GetBlogPostsCount(id),
                    BlogBusinessLogic.GetBlogFollowingCount(id)
                ));
            }
            return blogsInformationByName;
        }
    }
}
