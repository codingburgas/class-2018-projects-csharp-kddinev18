using DiabetesTracker.Logic;
using DiabetesTracker.Models;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TagsPage.xaml
    /// </summary>
    public partial class TagsPage : Page
    {
        // Data binding property for all tags information
        public ObservableCollection<CurrentTagInformation> TagsInformation { get; set; } = new ObservableCollection<CurrentTagInformation>();
        // Data binding property the selected tag information
        public CurrentTagInformation SelectedTagInformation { get; set; } = new CurrentTagInformation();

        private SocialMediaPage _socialMediaPage;
        public TagsPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            // Loads tag information into the data binding property TagsInformation
            SocialMediaLogic.LoadTagsInformation(TagsInformation);
            InitializeComponent();
            DataContext = this;
        }
        private void OnBlog_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Get the selected tag
            CurrentTagInformation tagInformation = (sender as ListBox).SelectedItem as CurrentTagInformation;
            TagPostsPage tagPostsPage = new TagPostsPage(tagInformation.TagId, _socialMediaPage);

            // Show TagPostsPage
            _socialMediaPage.ShowPage(tagPostsPage);
        }
    }
}
