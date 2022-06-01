using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for SocialMediaPage.xaml
    /// </summary>
    public partial class SocialMediaPage : Page
    {
        public PostsPage PostsPage { get; set; }
        public BlogsPage BlogsPage { get; set; }
        public FavouritesPage FavouritesPage { get; set; }
        public FollowingPage FollowingPage { get; set; }
        public TagsPage TagsPage { get; set; }
        public AnalyticsPage AnalyticsPage { get; set; }

        private MainWindow _mainWindow;
        public SocialMediaPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            PostsPage = new PostsPage(this);
            InitializeComponent();
            ShowPage(PostsPage);
        }

        public void ShowPage(Page page)
        {
            SocialMediaPageFrame.Content = page;
        }

        //Event handlers
        private void PostsButton_Click(object sender, RoutedEventArgs e)
        {
            PostsPage = new PostsPage(this);
            BlogsPage = null;
            FavouritesPage = null;
            FollowingPage = null;
            TagsPage = null;
            AnalyticsPage = null;

            ShowPage(PostsPage);
        }
        private void BlogsButton_Click(object sender, RoutedEventArgs e)
        {
            BlogsPage = new BlogsPage(this);
            FavouritesPage = null;
            FollowingPage = null;
            TagsPage = null;
            AnalyticsPage = null;

            ShowPage(BlogsPage);
        }
        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            FavouritesPage = new FavouritesPage();
            BlogsPage = null;
            FollowingPage = null;
            TagsPage = null;
            AnalyticsPage = null;

            ShowPage(FavouritesPage);
        }
        private void FollowingButton_Click(object sender, RoutedEventArgs e)
        {
            FollowingPage = new FollowingPage(this);
            BlogsPage = null;
            FavouritesPage = null;
            TagsPage = null;
            AnalyticsPage = null;

            ShowPage(FollowingPage);
        }
        private void TagsButton_Click(object sender, RoutedEventArgs e)
        {
            TagsPage = new TagsPage(this);
            BlogsPage = null;
            FavouritesPage = null;
            FollowingPage = null;
            AnalyticsPage = null;

            ShowPage(TagsPage);
        }
        private void AnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            AnalyticsPage = new AnalyticsPage();
            BlogsPage = null;
            FavouritesPage = null;
            FollowingPage = null;
            TagsPage = null;

            ShowPage(AnalyticsPage);
        }
    }
}
