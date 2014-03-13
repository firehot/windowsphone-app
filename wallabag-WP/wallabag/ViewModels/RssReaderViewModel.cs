using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO.IsolatedStorage;

namespace RssReader
{
    public class RssReaderViewModel
    {

        private AddRangeObservableCollection<FeedItemViewModel> _latestItems =
            new AddRangeObservableCollection<FeedItemViewModel>();

        private AddRangeObservableCollection<FeedItemViewModel> _favouriteItems =
            new AddRangeObservableCollection<FeedItemViewModel>();

        private AddRangeObservableCollection<FeedItemViewModel> _archiveItems =
            new AddRangeObservableCollection<FeedItemViewModel>();


        public RssReaderViewModel()
        {
            var userID = "1";
            if (IsolatedStorageSettings.ApplicationSettings.Contains("userID"))
            {
                userID = (string)IsolatedStorageSettings.ApplicationSettings["userID"];
            }

            var token = "0";
            if (IsolatedStorageSettings.ApplicationSettings.Contains("userID"))
            {
                token = (string)IsolatedStorageSettings.ApplicationSettings["token"];
            }

            Feeds = new ObservableCollection<FeedViewModel>() {
               new FeedViewModel() { FeedName = "Wallabag - Unread Items",
                   ParentModel = this,
                   FeedUrl = "http://tcit.fr/wallabag-dev/?feed&type=home&user_id="+userID+"&token="+token},
                new FeedViewModel() { FeedName = "Wallabag - Favorites Items",
                 ParentModel = this,
                   FeedUrl = "http://tcit.fr/wallabag-dev/?feed&type=fav&user_id="+userID+"&token="+token},
                new FeedViewModel() { FeedName = "Wallabag - Archive Items",
                 ParentModel = this,
                   FeedUrl = "http://tcit.fr/wallabag-dev/?feed&type=archive&user_id="+userID+"&token="+token},
            };
        }

        internal void UpdateLatestItems()
        {
            // update the latest items
            var latest = new ObservableCollection<FeedItemViewModel>();

            foreach (var item in Feeds.SelectMany(f => f.Items)
                                    /*.OrderByDescending(i => i.DatePublished)*/
                                    .Take(30))
            {
                if (item.ParentFeed.FeedName == "Wallabag - Unread Items")
                {
                latest.Add(item);
                }
            }
            LatestItems.Clear();
            LatestItems.AddRange(latest);
        }
        internal void UpdateFavourites()
        {
            // update the latest items
            var favs = new ObservableCollection<FeedItemViewModel>();
            foreach (var item in Feeds.SelectMany(f => f.Items)
                                   /*.OrderByDescending(i => i.DatePublished)*/
                                   .Take(30))
            {
                if (item.ParentFeed.FeedName == "Wallabag - Favorites Items")
                {
                    favs.Add(item);
                }
            }
            FavouriteItems.Clear();
            FavouriteItems.AddRange(favs);
        }

        internal void UpdateArchives()
        {
            // update the latest items
            var arch = new ObservableCollection<FeedItemViewModel>();
            foreach (var item in Feeds.SelectMany(f => f.Items)
                                   /*.OrderByDescending(i => i.DatePublished)*/
                                   .Take(30))
            {
                if (item.ParentFeed.FeedName == "Wallabag - Archive Items")
                {
                    arch.Add(item);
                }
            }
            ArchiveItems.Clear();
            ArchiveItems.AddRange(arch);
        }

        public AddRangeObservableCollection<FeedItemViewModel> LatestItems
        {
            get { return _latestItems; }
            set { _latestItems = value; NotifyPropertyChanged("LatestItems"); }
        }

        public AddRangeObservableCollection<FeedItemViewModel> FavouriteItems
        {
            get { return _favouriteItems; }
            set { _favouriteItems = value; NotifyPropertyChanged("FavouriteItems"); }
        }

        public AddRangeObservableCollection<FeedItemViewModel> ArchiveItems
        {
            get { return _archiveItems; }
            set { _favouriteItems = value; NotifyPropertyChanged("ArchiveItems"); }
        }

        public ObservableCollection<FeedViewModel> Feeds { get; private set; }

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