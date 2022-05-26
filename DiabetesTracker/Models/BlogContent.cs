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
    public class BlogContent : INotifyPropertyChanged
    {
        public bool BelongsToUser { get; set; }
        public bool IsFollowed { get; set; }

        private string _blogName;
        public string BlogName
        {
            get
            {
                return this._blogName;
            }
            set
            {
                if (this._blogName != value)
                {
                    this._blogName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private BitmapImage _blogImage;
        public BitmapImage BlogImage
        {
            get
            {
                return this._blogImage;
            }
            set
            {
                if (this._blogImage != value)
                {
                    this._blogImage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _blogTotalPosts;
        public int BlogTotalPosts
        {
            get
            {
                return this._blogTotalPosts;
            }
            set
            {
                if (this._blogTotalPosts != value)
                {
                    this._blogTotalPosts = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _blogTotalFollowers;
        public int BlogTotalFollowers
        {
            get
            {
                return this._blogTotalFollowers;
            }
            set
            {
                if (this._blogTotalFollowers != value)
                {
                    this._blogTotalFollowers = value;
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
                if (this._postContent != value)
                {
                    this._postContent = value;
                    NotifyPropertyChanged();
                }
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
                if (this._postImage != value)
                {
                    this._postImage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
