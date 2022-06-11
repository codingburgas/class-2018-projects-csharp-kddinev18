using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
#nullable disable

namespace DiabetesTracker.Models
{
    // POCO class used for data binding
    public class CurrentBlogInformation
    {
        public int BlogId { get; set; }
        public BitmapImage BlogImage { get; set; }
        public string BlogName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsFollowed { get; set; }
    }
}
