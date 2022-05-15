﻿using System;
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

namespace DiabetesTracker.ViewModels
{
    /// <summary>
    /// Interaction logic for BlogsPage.xaml
    /// </summary>
    
    public partial class BlogsPage : Page
    {
        public class BlogInformation
        {
            public BitmapImage BlogImage { get; set; }
            public string BlogName { get; set; }
            public int PostCount { get; set; }
            public int FollowingCount { get; set; }
        }
        public BlogsPage()
        {
            InitializeComponent();
        }
    }
}
