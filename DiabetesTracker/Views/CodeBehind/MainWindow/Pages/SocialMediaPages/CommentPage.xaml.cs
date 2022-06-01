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
        public CommentPage(int postId)
        {
            _postId = postId;
            InitializeComponent();
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
