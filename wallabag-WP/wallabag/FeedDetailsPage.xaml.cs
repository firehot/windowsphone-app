using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;

namespace RssReader
{
    public partial class FeedDetailsPage : PhoneApplicationPage
    {
        private FeedViewModel _viewModel;

        public FeedDetailsPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
            PageTransitionDetails.Completed += new EventHandler(PageTransitionDetails_Completed);
            Loaded += new RoutedEventHandler(PhoneApplicationPage_Loaded);
            PageTransitionList.Completed += new EventHandler(PageTransitionList_Completed);

            FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
            var currentFeed = root.DataContext as FeedViewModel;
            _viewModel = currentFeed;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.lstFeeds.SelectedIndex = -1;
        }

       
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            ResetPageTransitionList.Begin();
            this.DataContext = _viewModel;
        }
        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cancel default navigation
            e.Cancel = true;
            // Do page ransition animation
            PageTransitionDetails.Begin();
        }
        void PageTransitionDetails_Completed(object sender, EventArgs e)
        {
            // Reset root frame to MainPage.xaml
            PhoneApplicationFrame root = (PhoneApplicationFrame)Application.Current.RootVisual;
            root.GoBack();
        }


        private void PageTransitionList_Completed(object sender, EventArgs e)
        {
            // Set datacontext of details page to selected listbox item
            NavigationService.Navigate(new Uri("/FeedItemDetailsPage.xaml", UriKind.Relative));
        }

        private void lstFeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem == null)
                return;

            // Capture selected item data
            _viewModel.CurrentFeedItem = (sender as ListBox).SelectedItem as FeedItemViewModel;

            // Start page transition animation
            PageTransitionList.Begin();
        }

        private void MarkAllRead_Click(object sender, EventArgs e)
        {
            foreach (var item in _viewModel.Items)
            {
                item.Read = true;
            }
        }

        private void MarkAllUnRead_Click(object sender, EventArgs e)
        {
            foreach (var item in _viewModel.Items)
            {
                item.Read = false;
            }
        }
    }
}