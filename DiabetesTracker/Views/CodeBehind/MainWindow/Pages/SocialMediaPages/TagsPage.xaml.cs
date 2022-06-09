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
        public ObservableCollection<CurrentTagInformation> TagsInformation { get; set; } = new ObservableCollection<CurrentTagInformation>();
        public CurrentTagInformation SelectedTagInformation { get; set; } = new CurrentTagInformation();

        private SocialMediaPage _socialMediaPage;
        public TagsPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            SocialMediaLogic.LoadTagsInformation(TagsInformation);
            InitializeComponent();
            DataContext = this;
        }
        private void OnBlog_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            CurrentTagInformation tagInformation = (sender as ListBox).SelectedItem as CurrentTagInformation;
            TagPostsPage tagPostsPage = new TagPostsPage(tagInformation.TagId, _socialMediaPage);

            _socialMediaPage.ShowPage(tagPostsPage);
        }
    }
}
