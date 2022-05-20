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
using MaterialDesignThemes;
using MahApps.Metro.IconPacks;

namespace DiabetesTracker.ViewModels.UserControls
{
    /// <summary>
    /// Interaction logic for TextBoxForm.xaml
    /// </summary>
    public partial class TextBoxForm : UserControl
    {
        public TextBoxForm()
        {
            InitializeComponent();
        }

        public PackIconMaterialKind TextBoxIcon
        {
            get { return (PackIconMaterialKind)GetValue(TextBoxIconProperty); }
            set { SetValue(TextBoxIconProperty, value); }
        }

        public static readonly DependencyProperty TextBoxIconProperty = DependencyProperty.Register("TextBoxIcon", typeof(PackIconMaterialKind), typeof(TextBoxForm));

        public string TextBoxHintText
        {
            get { return (string)GetValue(TextBoxHintTextProperty); }
            set { SetValue(TextBoxHintTextProperty, value); }
        }

        public static readonly DependencyProperty TextBoxHintTextProperty = DependencyProperty.Register("TextBoxHintText", typeof(string), typeof(TextBoxForm));
    }
}
