﻿#pragma checksum "E:\Users\Thomas Citharel\Documents\GitHub\RSS Sample\wallabag\FeedDetailsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4E423E72824D04209D97B34ED62F9EEF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace RssReader {
    
    
    public partial class FeedDetailsPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard PageTransitionDetails;
        
        internal System.Windows.Media.Animation.Storyboard PageTransitionList;
        
        internal System.Windows.Media.Animation.Storyboard ResetPageTransitionList;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid TitleGrid;
        
        internal System.Windows.Controls.Grid ContentGrid;
        
        internal System.Windows.Controls.ListBox lstFeeds;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/wallabag;component/FeedDetailsPage.xaml", System.UriKind.Relative));
            this.PageTransitionDetails = ((System.Windows.Media.Animation.Storyboard)(this.FindName("PageTransitionDetails")));
            this.PageTransitionList = ((System.Windows.Media.Animation.Storyboard)(this.FindName("PageTransitionList")));
            this.ResetPageTransitionList = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ResetPageTransitionList")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitleGrid = ((System.Windows.Controls.Grid)(this.FindName("TitleGrid")));
            this.ContentGrid = ((System.Windows.Controls.Grid)(this.FindName("ContentGrid")));
            this.lstFeeds = ((System.Windows.Controls.ListBox)(this.FindName("lstFeeds")));
        }
    }
}
