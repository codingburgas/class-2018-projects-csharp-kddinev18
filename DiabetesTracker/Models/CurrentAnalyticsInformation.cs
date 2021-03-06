using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DiabetesTracker.Models
{
    // POCO class used for data binding
    public class CurrentAnalyticsInformation
    {
        public int PostCount { get; set; }
        public int Likes { get; set; }
        public int Followers { get; set; }
        public int BlogCount { get; set; }
        public string MostFollowedBlog { get; set; }
        public string LeastFollowedBlog { get; set; }
    }
}
