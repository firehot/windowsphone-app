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

namespace wallabag
{
    public partial class preferences : PhoneApplicationPage
    {
        public preferences()
        {
            InitializeComponent();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("updateAtStartup"))
            {
                checkBox1.IsChecked = (bool)IsolatedStorageSettings.ApplicationSettings["updateAtStartup"];
            }

            /*if (IsolatedStorageSettings.ApplicationSettings.Contains("numItems2Display"))
            {
                textBox1.Text = (string)IsolatedStorageSettings.ApplicationSettings["numItems2Display"];
            }*/

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // save pref
            IsolatedStorageSettings.ApplicationSettings["updateAtStartup"] = checkBox1.IsChecked;

            //IsolatedStorageSettings.ApplicationSettings["numItems2Display"] = textBox1.Text;

            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}