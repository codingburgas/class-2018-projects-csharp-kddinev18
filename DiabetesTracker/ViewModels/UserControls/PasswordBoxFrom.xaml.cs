using MahApps.Metro.IconPacks;
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

namespace DiabetesTracker.ViewModels.UserControls
{
    /// <summary>
    /// Interaction logic for PasswordTextBox.xaml
    /// </summary>
    public partial class PasswordBoxFrom : UserControl
    {
        public PasswordBoxFrom()
        {
            InitializeComponent();
        }
        public PackIconMaterialKind PasswordBoxIcon
        {
            get { return (PackIconMaterialKind)GetValue(PasswordBoxIconProperty); }
            set { SetValue(PasswordBoxIconProperty, value); }
        }

        public static readonly DependencyProperty PasswordBoxIconProperty = DependencyProperty.Register("PasswordBoxIcon", typeof(PackIconMaterialKind), typeof(TextBoxForm));

        public string PasswordBoxHintText
        {
            get { return (string)GetValue(PasswordBoxHintTextProperty); }
            set { SetValue(PasswordBoxHintTextProperty, value); }
        }

        public static readonly DependencyProperty PasswordBoxHintTextProperty = DependencyProperty.Register("PasswordBoxHintText", typeof(string), typeof(TextBoxForm));
    }
}
