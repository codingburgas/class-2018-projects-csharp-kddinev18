using DiabetesTracker.Logic;
using DiabetesTracker.Models;
using Microsoft.Win32;
using ServiceLayer;
using System;
using System.Collections.Generic;
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
#nullable disable

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for PostPostingPage.xaml
    /// </summary>
    public partial class PostPostingPage : Page
    {
        private SocialMediaPage _socialMediaPage;

        private string _selectedImagePath;
        private int _blogId;
        public PostPostingPage(SocialMediaPage socialMediaPage,int blogId)
        {
            _socialMediaPage = socialMediaPage;
            _blogId = blogId;
            InitializeComponent();
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {

            string tags = Tags.TextBox.Text is null ? "null" : Tags.TextBox.Text;
            string content = Content.TextBox.Text;

            if (_selectedImagePath == String.Empty)
                return;

            string image = string.Join(';', File.ReadAllBytes(_selectedImagePath));

            Services.Post(CurrentUserInformation.CurrentUserId.Value, _blogId, tags, content, image);

            _socialMediaPage.ShowPage(new PostsPage(_socialMediaPage));
        }
        private void ImagePicker_Click(object sender, RoutedEventArgs e)
        {
            Preview.Source = SocialMediaLogic.ImagePickerDialog(ref _selectedImagePath);
        }
    }
}
