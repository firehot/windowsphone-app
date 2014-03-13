using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RssReader
{
    public class FeedItemViewModel : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }


        private bool _favourite = false;
        public bool Favourite
        {
            get
            {
                return _favourite;
            }
            set
            {
                Read = false;
                _favourite = value;                
                ParentFeed.ParentModel.UpdateFavourites();
                NotifyPropertyChanged("Favourite");
            }
        }

        private bool _read = false;
        public bool Read
        {
            get
            {
                return _read;
            }
            set
            {
                if (Favourite)
                    return;
                _read = value;
                NotifyPropertyChanged("Read");
            }
        }

        private FeedViewModel _parentFeed;
        public FeedViewModel ParentFeed
        {
            get
            {
                return _parentFeed;
            }
            set
            {
                _parentFeed = value;
                NotifyPropertyChanged("ParentFeed");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }


        private string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                NotifyPropertyChanged("Url");
            }
        }


        private DateTime _datePublished;
        public DateTime DatePublished
        {
            get
            {
                return _datePublished;
            }
            set
            {
                _datePublished = value;
                NotifyPropertyChanged("DatePublished");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}