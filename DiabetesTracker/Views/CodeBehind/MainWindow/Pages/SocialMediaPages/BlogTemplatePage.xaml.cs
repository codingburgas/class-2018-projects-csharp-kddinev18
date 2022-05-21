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
using DiabetesTracker.Models;

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for BlogTemplatePage.xaml
    /// </summary>
    public partial class BlogTemplatePage : Page
    {
        private BlogContent _blogContent;
        public BlogTemplatePage(BlogContent BlogContent)
        {
            _blogContent = new BlogContent();
            InitializeComponent();
            DataContext = _blogContent;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
