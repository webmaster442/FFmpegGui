using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace FFmpeg.Gui.ViewModels
{
    internal class ObservableCollectionExt<T>: ObservableCollection<T>
    {
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
