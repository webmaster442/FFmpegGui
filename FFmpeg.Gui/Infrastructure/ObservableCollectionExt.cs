//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace FFmpeg.Gui.Infrastructure
{
    internal class ObservableCollectionExt<T> : ObservableCollection<T>
    {
        public ObservableCollectionExt() : base()
        {
        }

        public ObservableCollectionExt(IEnumerable<T> collection) : base(collection)
        {
        }

        public ObservableCollectionExt(List<T> list) : base(list)
        {
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items?.Any() != true)
                return;

            CheckReentrancy();
            foreach (var item in items)
            {
                Items.Add(item);
            }
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Items)));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
