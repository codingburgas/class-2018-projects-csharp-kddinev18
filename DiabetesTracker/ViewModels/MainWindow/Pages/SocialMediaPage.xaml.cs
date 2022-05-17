using BusinessLogicLayer;
using DiabetesTracker.Models;
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

namespace DiabetesTracker.ViewModels
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
        public SocialMediaPage()
        {
            PostsPage = new PostsPage();
            BlogsPage = new BlogsPage();
            FavouritesPage = new FavouritesPage();
            FollowingPage = new FollowingPage();
            TagsPage = new TagsPage();
            AnalyticsPage = new AnalyticsPage();
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
            ShowPage(PostsPage);
        }
        private void BlogsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(BlogsPage);
        }
        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(FavouritesPage);
        }
        private void FollowingButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(FollowingPage);
        }
        private void TagsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(TagsPage);
        }
        private void AnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(AnalyticsPage);
        }
    }
}
