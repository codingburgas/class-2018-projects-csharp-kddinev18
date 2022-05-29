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
    /// Interaction logic for InfoCard.xaml
    /// </summary>
    public partial class InfoCard : UserControl
    {
        public InfoCard()
        {
            InitializeComponent();
        }

        public PackIconMaterialKind InfoCardIcon
        {
            get { return (PackIconMaterialKind)GetValue(InfoCardIconProperty); }
            set { SetValue(InfoCardIconProperty, value); }
        }

        public static readonly DependencyProperty InfoCardIconProperty =
            DependencyProperty.Register("InfoCardIcon", typeof(PackIconMaterialKind), typeof(InfoCard));


        public string InfoCardTitle
        {
            get { return (string)GetValue(InfoCardTitleProperty); }
            set { SetValue(InfoCardTitleProperty, value); }
        }

        public static readonly DependencyProperty InfoCardTitleProperty =
            DependencyProperty.Register("InfoCardTitle", typeof(string), typeof(InfoCard));

        public string InfoCardText
        {
            get { return (string)GetValue(InfoCardTextProperty); }
            set { SetValue(InfoCardTextProperty, value); }
        }

        public static readonly DependencyProperty InfoCardTextProperty =
            DependencyProperty.Register("InfoCardText", typeof(string), typeof(InfoCard));
    }
}
