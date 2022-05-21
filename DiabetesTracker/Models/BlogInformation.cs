using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DiabetesTracker.Models
{
    public class BlogInformation
    {
        public int Id { get; set; }
        public BitmapImage BlogImage { get; set; }
        public string BlogName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
    }
}
