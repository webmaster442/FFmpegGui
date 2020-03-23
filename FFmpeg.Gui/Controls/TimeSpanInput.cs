using System;
using System.Windows;
using System.Windows.Controls;

namespace FFmpeg.Gui.Controls
{
    internal class TimeSpanInput : Control
    {
        public TimeSpan StartTime
        {
            get { return (TimeSpan)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof(TimeSpan), typeof(TimeSpanInput), new PropertyMetadata(TimeSpan.FromSeconds(0)));

        public TimeSpan EndTime
        {
            get { return (TimeSpan)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(TimeSpan), typeof(TimeSpanInput), new PropertyMetadata(TimeSpan.FromSeconds(0)));
    }
}
