using System;
using System.Linq;
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
using System.Collections.Generic;
using System.Collections.Specialized;

namespace RssReader
{
    // Custom class adds an efficient AddRange method for adding many items at once
    // without causing a CollectionChanged event for every item
    public class AddRangeObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suppressOnCollectionChanged;

        public void AddRange(IEnumerable<T> items)
        {
            if (null == items)
            {
                throw new ArgumentNullException("items");
            }
            if (items.Any())
            {
                try
                {
                    _suppressOnCollectionChanged = true;
                    foreach (var item in items)
                    {
                        Add(item);
                    }
                }
                finally
                {
                    _suppressOnCollectionChanged = false;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressOnCollectionChanged)
            {
                base.OnCollectionChanged(e);
            }
        }
    }
}
