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
            if (IsolatedStorageSettings.ApplicationSettings.Contains("token"))
            {
                token = (string)IsolatedStorageSettings.ApplicationSettings["token"];
            }
            var wallabagURL = "";
            if (IsolatedStorageSettings.ApplicationSettings.Contains("wallabag"))
            {
                wallabagURL = (string)IsolatedStorageSettings.ApplicationSettings["wallabag"];
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains("framabaguser"))
            {
                wallabagURL = "https://www.framabag.org/u/" + (string)IsolatedStorageSettings.ApplicationSettings["framabaguser"] + "/";
            }

            Feeds = new ObservableCollection<FeedViewModel>() {
               new FeedViewModel() { FeedName = "Wallabag - Unread Items",
                   ParentModel = this,
                   FeedUrl = wallabagURL+"?feed&type=home&user_id="+userID+"&token="+token},
                new FeedViewModel() { FeedName = "Wallabag - Favorites Items",
                 ParentModel = this,
                   FeedUrl = wallabagURL+"?feed&type=fav&user_id="+userID+"&token="+token},
                new FeedViewModel() { FeedName = "Wallabag - Archive Items",
                 ParentModel = this,
                   FeedUrl = wallabagURL+"?feed&type=archive&user_id="+userID+"&token="+token},
            };
        }

        private int getNumItems()
        {
            var numItems2Display = "30";
            var numIntItems2Display = 30;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("numItems2Display"))
            {
                numItems2Display = (string)IsolatedStorageSettings.ApplicationSettings["numItems2Display"];
                int.TryParse(numItems2Display, out numIntItems2Display);
            }
            return numIntItems2Display;
        }


        internal void UpdateLatestItems()
        {

            

            // update the latest items
            var latest = new ObservableCollection<FeedItemViewModel>();

            foreach (var item in Feeds.SelectMany(f => f.Items)
                                    /*.OrderByDescending(i => i.DatePublished)*/
                                    .Take(getNumItems()))
            {
                if (item.ParentFeed.FeedName == "Wallabag - Unread Items")
                {
                latest.Add(item);
                }
            }
            LatestItems.Clear();
            LatestItems.AddRange(latest);
            //MessageBox.Show(LatestItems.Count().ToString());
        }
        internal void UpdateFavourites()
        {
            // update the latest items
            var favs = new ObservableCollection<FeedItemViewModel>();
            foreach (var item in Feeds.SelectMany(f => f.Items)
                                   .OrderByDescending(i => i.DatePublished)
                                   .Take(getNumItems()))
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
                                   .OrderByDescending(i => i.DatePublished)
                                   .Take(getNumItems()))
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