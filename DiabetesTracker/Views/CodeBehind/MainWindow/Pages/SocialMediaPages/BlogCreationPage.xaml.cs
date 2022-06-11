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
    /// Interaction logic for BlogCreationPage.xaml
    /// </summary>
    public partial class BlogCreationPage : Page
    {
        private SocialMediaPage _socialMediaPage;

        private string _selectedImagePath;
        public BlogCreationPage(SocialMediaPage socialMediaPage)
        {
            _socialMediaPage = socialMediaPage;
            InitializeComponent();
        }

        private void CreateBlogButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the blog name from BlogName text box
            string blogName = BlogName.TextBox.Text;

            // Checks the blogname
            if (!HandleUserInput.GeneralHandler(blogName))
                return;

            // Gets the bytes of the selected image
            string image = string.Join(';', File.ReadAllBytes(_selectedImagePath));

            // Created a blog
            Services.CreateBlog(CurrentUserInformation.CurrentUserId.Value, image, blogName);

            // Shows BlogsPage
            _socialMediaPage.ShowPage(new BlogsPage(_socialMediaPage));
        }
        private void ImagePicker_Click(object sender, RoutedEventArgs e)
        {
            // Show a preview of the selcted image
            Preview.Source = SocialMediaLogic.ImagePickerDialog(ref _selectedImagePath);
        }
    }
}
