#define Debug

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.IO.IsolatedStorage;

namespace RssReader
{
    public partial class MainPage : PhoneApplicationPage
    {
        object _selectedItem;
        RssReaderViewModel model;

        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait;
            Loaded += new RoutedEventHandler(MainPage_Loaded);
            PageTransitionList.Completed += new EventHandler(PageTransitionList_Completed);

            model = new RssReaderViewModel();
            DataContext = model;

            
            var update = true;
            var load = true;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("firstStart"))
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("updateAtStartup"))
                {
                    if ((bool)IsolatedStorageSettings.ApplicationSettings["updateAtStartup"] != true)
                    {
                        update = false;
                    }
                }
            }
            else
            {
                load = false;
                MessageBox.Show("You must first set your server configuration. See bar below.");
            }

            if (load)
            {
                model.Feeds[0].LoadFeed(update);
                model.Feeds[1].LoadFeed(update);
                model.Feeds[2].LoadFeed(update);
            }
            /*if (!IsolatedStorageSettings.ApplicationSettings.Contains(model.Feeds[0].FeedName)) {
                model.Feeds[0].LoadFeed();
                MessageBox.Show("here");
            }
            MessageBox.Show(model.Feeds[0].FeedName);
            model.Feeds[0].proceed((string)IsolatedStorageSettings.ApplicationSettings[model.Feeds[0].FeedName]);
            */
            /*foreach (var feed in model.Feeds)
            {
                feed.LoadFeed();
                Update.Text = feed.FeedName;
            }*/
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //this.ListBoxOne.SelectedIndex = -1;
            this.ListBoxTwo.SelectedIndex = -1;
            this.ListBoxThree.SelectedIndex = -1;
            this.ListBoxFour.SelectedIndex = -1;
        }

        private void PageTransitionList_Completed(object sender, EventArgs e)
        {
            if (navigateToArticle)
            {
                // Set datacontext of details page to selected listbox item
                NavigationService.Navigate(new Uri("/FeedItemDetailsPage.xaml", UriKind.Relative));
                
            }
            else
            {
                // Set datacontext of details page to selected listbox item
                NavigationService.Navigate(new Uri("/FeedDetailsPage.xaml", UriKind.Relative));
            };

            FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
            root.DataContext = _selectedItem;
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Reset page transition
            ResetPageTransitionList.Begin();

        }

        private bool navigateToArticle = false;

        /*private void ListBoxOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Capture selected item data
            _selectedItem = (sender as ListBox).SelectedItem;
            if (_selectedItem == null)
                return;

            navigateToArticle = false;

            // Start page transition animation
            PageTransitionList.Begin();
        }*/

        private void lstLatestItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Capture selected item data
            var selectedItem = (sender as ListBox).SelectedItem as FeedItemViewModel;
            if (selectedItem == null)
                return;

            selectedItem.ParentFeed.CurrentFeedItem = selectedItem;
            _selectedItem = selectedItem.ParentFeed;

            // Start page transition animation
            navigateToArticle = true;
            PageTransitionList.Begin();
        }

        private void favFavouritesItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Capture selected item data
            var selectedItem = (sender as ListBox).SelectedItem as FeedItemViewModel;
            if (selectedItem == null)
                return;

            selectedItem.ParentFeed.CurrentFeedItem = selectedItem;
            _selectedItem = selectedItem.ParentFeed;

            // Start page transition animation
            navigateToArticle = true;
            PageTransitionList.Begin();
        }

        private void archiveItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Capture selected item data
            var selectedItem = (sender as ListBox).SelectedItem as FeedItemViewModel;
            if (selectedItem == null)
                return;

            selectedItem.ParentFeed.CurrentFeedItem = selectedItem;
            _selectedItem = selectedItem.ParentFeed;

            // Start page transition animation
            navigateToArticle = true;
            PageTransitionList.Begin();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/addArticle.xaml", UriKind.Relative));
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            model.Feeds[0].LoadFeed(true);
            model.Feeds[1].LoadFeed(true);
            model.Feeds[2].LoadFeed(true);
        }

        private void Conf_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/configuration.xaml", UriKind.Relative));
        }

        private void Pref_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/preferences.xaml", UriKind.Relative));
        }
        
    }
}