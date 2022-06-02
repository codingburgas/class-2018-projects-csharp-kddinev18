using DiabetesTracker.Models;
using ServiceLayer;
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

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for CommentPage.xaml
    /// </summary>
    public partial class CommentPage : Page
    {
        private int _postId;
        private List<CurrentCommentInformation> _comments = new List<CurrentCommentInformation>();
        public CommentPage(int postId)
        {
            _postId = postId;
            LoadComments(_postId);
            InitializeComponent();
            DataContext = _comments;
        }
        
        private void LoadComments(int postId)
        {
            foreach (string comment in Services.GetComments(CurrentUserInformation.CurrentUserId.Value, postId))
            {
                _comments.Add(new CurrentCommentInformation()
                {
                    Comment = comment
                }); 
            }
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (Comment.TextBox.Text is null)
                return;

            Services.Comment(CurrentUserInformation.CurrentUserId.Value, _postId, Comment.TextBox.Text);

            Comment.TextBox.Text = "";
        }
    }
}
