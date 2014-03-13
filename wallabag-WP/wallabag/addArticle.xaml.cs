using System;
using System.Collections.Generic;
using System.Text;
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
using System.IO.IsolatedStorage;

namespace RssReader
{
    public partial class addArticle : PhoneApplicationPage
    {
        public addArticle()
        {
            InitializeComponent();
        }

        private void addArticleClick(object sender, RoutedEventArgs e)
        {
            var userwallabag = "";
            // send URL to method
            var url = "http://" + textBox1.Text;
            if (url.StartsWith("http://") || url.StartsWith("https://")) {
                url = textBox1.Text;
            }
            // encode url
            var encodedurl = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(url));
            //get user's wallabag
            // if framabag
            if (IsolatedStorageSettings.ApplicationSettings.Contains("framabaguser"))
            {
                userwallabag = "https://www.framabag.org/u/" + (string)IsolatedStorageSettings.ApplicationSettings["framabaguser"]+"/";
            }
                //if custom wallabag
            else if (IsolatedStorageSettings.ApplicationSettings.Contains("wallabag"))
            {
                userwallabag = (string)IsolatedStorageSettings.ApplicationSettings["wallabag"];
            }
                // if not configurated
            else {
                var setup = MessageBox.Show("You haven't set up your wallabag account. Do you want to do it now ?", "Error", MessageBoxButton.OKCancel);
                if (setup == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/preferences.xaml", UriKind.Relative));
                }
            }
            if (userwallabag != "")
            {
                var finalURL = userwallabag + "?action=add&url=" + encodedurl;
                //MessageBox.Show(finalURL);
                webBrowser1.Navigate(new Uri(finalURL));
            }
        }
    }
}