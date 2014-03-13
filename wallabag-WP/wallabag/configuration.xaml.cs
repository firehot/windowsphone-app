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
using System.IO.IsolatedStorage;

namespace RssReader
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            
            if (IsolatedStorageSettings.ApplicationSettings.Contains("wallabag"))
            {
                textBox1.Text = (string)IsolatedStorageSettings.ApplicationSettings["wallabag"];
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains("framabaguser"))
            {
                textBox2.Text = (string)IsolatedStorageSettings.ApplicationSettings["framabaguser"];
            }

            if (IsolatedStorageSettings.ApplicationSettings.Contains("userID"))
            {
                textBox3.Text = (string)IsolatedStorageSettings.ApplicationSettings["userID"];
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains("token"))
            {
                textBox4.Text = (string)IsolatedStorageSettings.ApplicationSettings["token"];
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
                    // wallabag
            
                    if (textBox1.Text != "" && textBox2.Text == "")
                    {
                        var wallabagURL = "http://" + textBox1.Text;
                        if (textBox1.Text.StartsWith("http://") || textBox1.Text.StartsWith("https://"))
                        {
                            wallabagURL = textBox1.Text;
                        }
                        IsolatedStorageSettings.ApplicationSettings["wallabag"] = wallabagURL;
                        if (IsolatedStorageSettings.ApplicationSettings.Contains("framabaguser"))
                        {
                            IsolatedStorageSettings.ApplicationSettings.Remove("framabaguser");
                        }
                }
            // framabag
                    else if (textBox2.Text != "" && textBox1.Text == "")
                    {
                        IsolatedStorageSettings.ApplicationSettings["framabaguser"] = textBox2.Text;
                        if (IsolatedStorageSettings.ApplicationSettings.Contains("wallabag"))
                        {
                            IsolatedStorageSettings.ApplicationSettings.Remove("wallabag");
                        }
                    }
            //userID
                    if (textBox3.Text != "")
                    {
                        IsolatedStorageSettings.ApplicationSettings["userID"] = textBox3.Text;
                    }
            //token
                    if (textBox4.Text != "")
                    {
                        IsolatedStorageSettings.ApplicationSettings["token"] = textBox4.Text;
                    }

                    if (!IsolatedStorageSettings.ApplicationSettings.Contains("firstStart"))
                    {
                        IsolatedStorageSettings.ApplicationSettings["firstStart"] = true;
                    }
            IsolatedStorageSettings.ApplicationSettings.Save();
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox4_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox4.Text = "";
        }
    }
}