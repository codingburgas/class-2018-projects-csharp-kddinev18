using DiabetesTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
#nullable disable

namespace DiabetesTracker.Models
{
    // POCO class used for data binding
    public class CurrentPostInformation : INotifyPropertyChanged
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
        private bool _isPostLiked;
        public bool IsPostLiked
        {
            get
            {
                return this._isPostLiked;
            }
            set
            {
                this._isPostLiked = value;
                NotifyPropertyChanged();
            }
        }
        private bool _isPostFavourited;
        public bool IsPostFavourited
        {
            get
            {
                return this._isPostFavourited;
            }
            set
            {
                this._isPostFavourited = value;
                NotifyPropertyChanged();
            }
        }

        // Event that will be invoked every time a property changes value
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            // if the PropertyChanged is not null invoke it
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
