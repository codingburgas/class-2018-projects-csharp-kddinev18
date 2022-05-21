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

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for BlogTemplatePage.xaml
    /// </summary>
    public partial class BlogTemplatePage : Page
    {
        public string BlogName { get; set; }
        public BitmapImage BlogImage { get; set; }
        public string BlogTotalPosts { get; set; }
        public string BlogTotalFollowers { get; set; }

        public string PostContent { get; set; }
        public BitmapImage PostImage { get; set; }
        public BlogTemplatePage()
        {
            InitializeComponent();
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
