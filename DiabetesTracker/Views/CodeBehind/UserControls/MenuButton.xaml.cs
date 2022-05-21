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
using MahApps.Metro.IconPacks;

namespace DiabetesTracker.ViewModels.UserControls
{
    /// <summary>
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            InitializeComponent();
        }

        public PackIconMaterialKind MenuButtonIcon
        {
            get { return (PackIconMaterialKind)GetValue(MenuButtonIconProperty); }
            set { SetValue(MenuButtonIconProperty, value); }
        }

        public static readonly DependencyProperty MenuButtonIconProperty =
            DependencyProperty.Register("MenuButtonIcon", typeof(PackIconMaterialKind), typeof(MenuButton));


        public string MenuButtonText
        {
            get { return (string)GetValue(MenuButtonTextProperty); }
            set { SetValue(MenuButtonTextProperty, value); }
        }

        public static readonly DependencyProperty MenuButtonTextProperty =
            DependencyProperty.Register("MenuButtonText", typeof(string), typeof(MenuButton));
    }
}
