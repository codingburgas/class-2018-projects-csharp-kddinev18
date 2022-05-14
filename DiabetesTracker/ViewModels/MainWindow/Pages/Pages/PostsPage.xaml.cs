using DiabetesTracker.Models;
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
    public partial class PostsPage : Page
    {
        public List<Tuple<string, string, BitmapImage>> Posts { get; set; } = MainWindowModel.GetPosts(0);
        private int _index = 0;
        public PostsPage()
        {
            InitializeComponent();
            SetPost(_index++);
        }
        private void SetPost(int index)
        {
            UserNameTextBlock.Text = Posts.ElementAt(index).Item1;
            ContentTextBlock.Text = Posts.ElementAt(index).Item2;
            PostImage.Source = Posts.ElementAt(index).Item3;
        }
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_index <= 0)
                return;
            if(_index % 10 == 0)
            {
                Posts = MainWindowModel.GetPosts(_index - 10);
            }
            _index--;
            SetPost(_index % 10);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(_index % 10 == Posts.Count-1)
            {
                try
                {
                    Posts = MainWindowModel.GetPosts(_index+1);
                }
                catch (ArgumentNullException)
                {
                    return;
                }
            }
            _index++;
            SetPost(_index % 10);
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
