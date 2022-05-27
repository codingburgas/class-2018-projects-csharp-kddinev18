using Microsoft.Win32;
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

namespace DiabetesTracker.Views
{
    /// <summary>
    /// Interaction logic for PostPostingPage.xaml
    /// </summary>
    public partial class PostPostingPage : Page
    {
        private string _selectedImagePath;
        public PostPostingPage()
        {
            InitializeComponent();
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            string []tags = Tags.TextBox.Text.Split(',');
            string content = Content.TextBox.Text;

            if (_selectedImagePath == String.Empty)
                return;

            byte[] imageArray = File.ReadAllBytes(_selectedImagePath);
        }
        private void ImagePicker_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string _selectedImagePath = dlg.FileName;
                Preview.Source = new BitmapImage(new Uri(_selectedImagePath));
            }
        }
    }
}
