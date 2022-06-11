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
        // The current post id
        private int _postId;
        // Data binding property for all the comments on the post id
        public ObservableCollection<CurrentCommentInformation> Comments { get; set; } = new ObservableCollection<CurrentCommentInformation>();

        public CommentPage(int postId)
        {
            _postId = postId;
            // Load the data binding property Comments with comments
            SocialMediaLogic.LoadComments(Comments,_postId);
            InitializeComponent();

            // Set the item source of the list view to the data binding property Comments
            CommentsList.ItemsSource = Comments;
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            // Add comments
            SocialMediaLogic.Comment(Comment.TextBox.Text, _postId);
            // Resets the comments
            Comments = new ObservableCollection<CurrentCommentInformation>();
            // Load the comments again
            SocialMediaLogic.LoadComments(Comments, _postId);
            // Set the item source of the list view to the data binding property Comments
            CommentsList.ItemsSource = Comments;

            // Reset the comment text box
            Comment.TextBox.Text = "";
        }
    }
}
