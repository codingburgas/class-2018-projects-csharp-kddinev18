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

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for CommentPage.xaml
    /// </summary>
    public partial class CommentPage : Page
    {
        private int _postId;
        public ObservableCollection<CurrentCommentInformation> Comments { get; set; } = new ObservableCollection<CurrentCommentInformation>();

        public CommentPage(int postId)
        {
            _postId = postId;
            SocialMediaLogic.LoadComments(Comments,_postId);
            InitializeComponent();
            CommentsList.ItemsSource = Comments;
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            SocialMediaLogic.Comment(Comment.TextBox.Text, _postId);
            Comments = new ObservableCollection<CurrentCommentInformation>();
            SocialMediaLogic.LoadComments(Comments, _postId);
            CommentsList.ItemsSource = Comments;

            Comment.TextBox.Text = "";
        }
    }
}
