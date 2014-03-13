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
using System.CodeDom;
using System.Diagnostics;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Text.RegularExpressions;

namespace RssReader
{
    public partial class FeedItemDetailsPage : PhoneApplicationPage
    {
        public FeedItemDetailsPage()
        {
            InitializeComponent();

            PageTransitionDetails.Completed += new EventHandler(PageTransitionDetails_Completed);

            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;

            Loaded += (s,e) => LoadStory();
        }

        /// <summary>
        /// Loads the current RSS article into the WebBrowser
        /// </summary>
        private void LoadStory()
        {
            FeedViewModel vm = this.DataContext as FeedViewModel;
            browser.NavigateToString(ConvertExtendedASCII(AddStyledBody(vm.CurrentFeedItem.Description)));
            Debug.WriteLine(vm.CurrentFeedItem.Description);

            vm.CurrentFeedItem.Read = true;
            UpdateButtonEnabled();
        }

        private string AddStyledBody(string content)
        {
            return "<html>" + 
                "<head><style type=\"text/css\">a { color: #aaf;}</style></head>" +
                "<body style=\"background-color: black; color: white;\">" + content+
                "</body></html>";
        }

        private void UpdateButtonEnabled()
        {
            FeedViewModel vm = this.DataContext as FeedViewModel;
            int itemIndex = vm.Items.IndexOf(vm.CurrentFeedItem);

            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = itemIndex > 0;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = itemIndex < vm.Items.Count - 1;
        }

        /// <summary>
        /// use entity codes for any characters that are not in the standard ASCII set
        /// </summary>
        public static string ConvertExtendedASCII(string html)
        {
          string retVal = "";
          char[] s = html.ToCharArray();

          foreach (char c in s)
          {
            if (Convert.ToInt32(c) > 127)
              retVal += "&#" + Convert.ToInt32(c) + ";";
            else
              retVal += c;
          }

          return retVal;
        }


        // Handle navigating back to content in the two frames
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
            //PhoneApplicationFrame root = (PhoneApplicationFrame)Application.Current.RootVisual;
            //root.GoBack();
            NavigationService.GoBack();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            FeedViewModel vm = this.DataContext as FeedViewModel;
            int itemIndex = vm.Items.IndexOf(vm.CurrentFeedItem);
            itemIndex = Math.Min(vm.Items.Count - 1, itemIndex + 1);
            vm.CurrentFeedItem = vm.Items[itemIndex];
            LoadStory();
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            FeedViewModel vm = this.DataContext as FeedViewModel;
            int itemIndex = vm.Items.IndexOf(vm.CurrentFeedItem);
            itemIndex = Math.Max(0, itemIndex-1);
            vm.CurrentFeedItem = vm.Items[itemIndex];
            LoadStory();
        }

        private void Favourite_Click(object sender, EventArgs e)
        {
            FeedViewModel vm = this.DataContext as FeedViewModel;
            vm.CurrentFeedItem.Favourite = true;
        }

        /*private void SendAsSMS_Click(object sender, EventArgs e)
        {
            FeedViewModel vm = this.DataContext as FeedViewModel;

            string noTags = Regex.Replace(vm.CurrentFeedItem.Description, "<.*?>", string.Empty);
            if (noTags.Length > 255)
                noTags = noTags.Substring(0, 250) + "...";

            SmsComposeTask smsComposeTask = new SmsComposeTask();
            smsComposeTask.Body = noTags;
            smsComposeTask.Show();
        }*/

        
    }
}