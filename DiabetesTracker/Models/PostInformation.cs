using DiabetesTracker.ViewModels;
using System;
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
        public static PostInformation operator+(PostInformation postInformation, Tuple<int, string, string, byte[], bool, bool> tuple)
        {
            postInformation = new PostInformation()
            {
                BlogName = tuple.Item2,
                PostContent = tuple.Item3,
                PostImage = PostsPage.ConvertByteArrayToBitMapImage(tuple.Item4),
                IsPostLiked = tuple.Item5,
                IsPostFavourited = tuple.Item6,
            };
            return postInformation;
        }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
