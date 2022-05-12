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

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public class PostContainer
        {
            public string User { get; set; }
            public Image Image { get; set; }
            public string Content { get; set; }
        }
        public ObservableCollection<PostContainer> Posts { get; set; } = new ObservableCollection<PostContainer>();
        public Home()
        {
            InitializeComponent();
            this.DataContext = this;
            Posts.Add(new PostContainer() { Content="aaaaa", User="bbbb", Image=new Image()});
            Posts.Add(new PostContainer() { Content="aaaaa", User="bbbb", Image=new Image()});
            Posts.Add(new PostContainer() { Content="aaaaa", User="bbbb", Image=new Image()});
            Posts.Add(new PostContainer() { Content="aaaaa", User="bbbb", Image=new Image()});
            PostList.ItemsSource = Posts;
        }
    }
}
