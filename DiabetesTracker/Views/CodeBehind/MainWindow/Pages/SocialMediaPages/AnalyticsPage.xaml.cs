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
#nullable disable

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for AnalyticsPage.xaml
    /// </summary>
    public partial class AnalyticsPage : Page
    {
        // Property used for data binding
        public CurrentAnalyticsInformation CurrentAnalyticsInformation { get; set; }
        public AnalyticsPage()
        {
            InitializeComponent();
            SetUpAnalyticsData();

            // Set the DataContext to the data binding property
            DataContext = CurrentAnalyticsInformation;
        }

        // Loads the data binding property with analytics information
        public void SetUpAnalyticsData()
        {
            // Get the analytics information
            AnalyticsInformation analyticsInformation = Services.GetAnalyticsInformation(CurrentUserInformation.CurrentUserId.Value);

            // Load the data binding property
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
