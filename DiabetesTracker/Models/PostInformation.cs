﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DiabetesTracker.Models
{
    public class PostInformation : INotifyPropertyChanged
    {
        private string _blogName;
        public string BlogName
        {
            get
            {
                return this._blogName;
            }
            set
            {
                if (_blogName != value)
                {
                    this._blogName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _postContent;
        public string PostContent
        {
            get
            {
                return this._postContent;
            }
            set
            {
                this._postContent = value;
                NotifyPropertyChanged();
            }
        }
        private BitmapImage _postImage;
        public BitmapImage PostImage
        {
            get
            {
                return this._postImage;
            }
            set
            {
                this._postImage = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
