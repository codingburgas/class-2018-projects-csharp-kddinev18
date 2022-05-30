using DiabetesTracker.Models;
using ServiceLayer;
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

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for AnalyticsPage.xaml
    /// </summary>
    public partial class AnalyticsPage : Page
    {
        public CurrentAnalyticsInformation CurrentAnalyticsInformation { get; set; }
        public AnalyticsPage()
        {
            InitializeComponent();
            SetUpAnalyticsData();
            DataContext = CurrentAnalyticsInformation;
        }

        public void SetUpAnalyticsData()
        {
            AnalyticsInformation analyticsInformation = Services.GetAnalyticsInformation(CurrentUserInformation.CurrentUserId.Value);
            CurrentAnalyticsInformation = new CurrentAnalyticsInformation() { 
                PostCount = analyticsInformation.PostCount,
                Likes = analyticsInformation.Likes,
                Followers = analyticsInformation.Followers,
                BlogCount = analyticsInformation.BlogCount,
                MostFollowedBlog = analyticsInformation.MostFollowedBlog,
                LeastFollowedBlog = analyticsInformation.LeastFollowedBlog
            };
        }
    }
}
