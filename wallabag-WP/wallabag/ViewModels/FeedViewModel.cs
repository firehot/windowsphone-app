using System;
using System.Linq;
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
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.IO.IsolatedStorage;
using System.IO;


namespace RssReader
{
    public class FeedViewModel : INotifyPropertyChanged
    {

        private FeedItemViewModel _currentFeedItem;

        private string _feedName;

        private string _feedUrl;

        private string _feedStatus;

        private bool _isFavourite;

        private int _unReadItems;

        //private string file;

        public int UnReadItems
        {
            get { return _unReadItems; }
            set { _unReadItems = value; NotifyPropertyChanged("UnReadItems"); }
        }

        private RssReaderViewModel _parentModel = null;

        public RssReaderViewModel ParentModel
        {
            get { return _parentModel; }
            set { _parentModel = value; NotifyPropertyChanged("ParentModel"); }
        }

        private bool _read = false;

        public bool Read
        {
            get { return _read; }
            set { _read = value; NotifyPropertyChanged("Read"); }
        }

        public bool IsFavourite
        {
            get {
                if (FeedName == "Wallabag - Unread Items")
                {
                    return false;
                }
                else { return true; }
            }
            set { _isFavourite = value; NotifyPropertyChanged("IsFavourite"); }
        }

        public string FeedStatus
        {
            get { return _feedStatus; }
            set { _feedStatus = value; NotifyPropertyChanged("FeedStatus");  }
        }

        public FeedItemViewModel CurrentFeedItem
        {
            get { return _currentFeedItem; }
            set { _currentFeedItem = value; NotifyPropertyChanged("CurrentFeedItem"); }
        }

        public string FeedName
        {
            get
            {
                return _feedName;
            }
            set
            {
                _feedName = value;
                NotifyPropertyChanged("FeedName");
            }
        }

        public string FeedUrl
        {
            get
            {
                return _feedUrl;
            }
            set
            {
                _feedUrl = value;
                NotifyPropertyChanged("FeedUrl");
            }
        }

        public FeedViewModel()
        {
            FeedStatus = "Loading ...";
            Items = new ObservableCollection<FeedItemViewModel>();
        }


        public void LoadFeed(bool askForUpdate)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            
            if (askForUpdate)
            {
                    this.DownloadFeed();
            }
            else
            {
                // offline
                IsolatedStorageFileStream stream = store.OpenFile("Save" + this._feedName, FileMode.Open);
                //MessageBox.Show("here");
                StreamReader reader = new StreamReader(stream);
                String file = reader.ReadToEnd();
                MessageBox.Show(file);
                parse(file);
                reader.Close();
            }
        }

        private void savefile(String file)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            if (store.FileExists("Save" + this._feedName))
            {
                store.DeleteFile("Save" + this._feedName);
            }
            IsolatedStorageFileStream stream = store.OpenFile("Save" + this._feedName, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            //MessageBox.Show(file);
            writer.Write(file);
            writer.Close();
        }

        private void DownloadFeed()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            //MessageBox.Show(FeedUrl);
            wc.DownloadStringAsync(new Uri(FeedUrl));

        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var file = e.Result;
            //MessageBox.Show("In the downloadcompleted : " + file);
            savefile(file);
            parse(file);
        }

        private void parse(string File){
            //MessageBox.Show(File);
            var rssFeed = XElement.Parse(File);
            Debug.WriteLine(rssFeed);
            Items.Clear();
            var items = from item in rssFeed.Descendants("item")
                        select new FeedItemViewModel()
                        {
                            Title = item.Element("title").Value,
                            DatePublished = DateTime.Parse(item.Element("pubDate").Value),
                            Url = item.Element("link").Value,
                            Description = item.Element("description").Value,
                            ParentFeed = this
                        };
            
            foreach (var item in items)
            {
                item.PropertyChanged += (s, e2) => UpdateFeedStatus();
                Items.Add(item);
            }

            ParentModel.UpdateLatestItems();
            ParentModel.UpdateFavourites();
            ParentModel.UpdateArchives();
            UpdateFeedStatus();           
        }

        private void UpdateFeedStatus()
        {
            if (!Items.Any(item => item.Read/* || item.Favourite*/))
            {
                Read = false;
                UnReadItems = Items.Count;
                FeedStatus = string.Format("{0} unread items", Items.Count);
            }
            else
            {
                int readItems = Items.Where(item => item.Read/* || item.Favourite*/).Count();
                UnReadItems = Items.Count - readItems;
                FeedStatus = string.Format("{0} unread, {1} items", UnReadItems, Items.Count);

                Read = readItems == Items.Count;
            }
        }

        public ObservableCollection<FeedItemViewModel> Items { get; set; }

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