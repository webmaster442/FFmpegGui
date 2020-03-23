using System;
using System.Windows;
using System.Windows.Controls;

namespace FFmpeg.Gui.Controls
{
    internal class TimeSpanInput : Control
    {
        public TimeSpanInputViewModel ViewModel { get; }

        public TimeSpanInput()
        {
            ViewModel = new TimeSpanInputViewModel();
            DataContext = ViewModel;
        }
    }
}
