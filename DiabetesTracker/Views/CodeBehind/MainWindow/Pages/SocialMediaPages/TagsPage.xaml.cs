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
            LoadTagsInformation();
            InitializeComponent();
            DataContext = this;
        }
        private void LoadTagsInformation()
        {
            List<TagInformation> tagsInformation = Services.GetTags();
            foreach (TagInformation tagInformation in tagsInformation)
            {
                TagsInformation.Add(new CurrentTagInformation()
                {
                    TagId = tagInformation.TagId,
                    TagName = tagInformation.TagName,
                    PostCount = tagInformation.PostCount,
                });
            }
        }
        private void OnBlog_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            CurrentTagInformation blogInformation = (sender as ListBox).SelectedItem as CurrentTagInformation;
            /*BlogContent blogContent = new BlogContent()
            {
                BlogName = blogInformation.BlogName,
                BlogImage = blogInformation.BlogImage,
                BlogTotalFollowers = blogInformation.FollowingCount,
                BlogTotalPosts = blogInformation.PostCount,
                BelongsToUser = Services.BelongsToUser(CurrentUserInformation.CurrentUserId.Value, blogInformation.BlogId),
                IsFollowed = blogInformation.IsFollowed
            };
            _socialMediaPage.ShowPage(new BlogTemplatePage(blogContent, _socialMediaPage, blogInformation.BlogId));*/
        }
    }
}
