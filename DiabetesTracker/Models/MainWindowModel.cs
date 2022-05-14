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
        public static List<Tuple<string,string,BitmapImage>> GetPosts(int skip)
        {
            List<int> ids = PostBusinessLogic.GetPostIds(skip);

            List<Tuple<string, string, BitmapImage>> posts = new List<Tuple<string, string, BitmapImage>>();

            foreach (int id in ids)
            {
                posts.Add(new Tuple<string, string, BitmapImage>(
                    PostBusinessLogic.GetPostUser(id), 
                    PostBusinessLogic.GetPostContent(id),
                    ConvertByteArrayToBitMapImage(PostBusinessLogic.GetPostImage(id))
                ));
            }

            return posts;
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
    }
}
