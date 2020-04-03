//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FFmpeg.Gui.Infrastructure
{
    internal static class WpfExtensionMethods
    {
        public static BitmapSource ToBitmapSource(this FrameworkElement source, double dpiX = 96, double dpiY = 96)
        {
            if (source == null) return null;

            Rect bounds = VisualTreeHelper.GetDescendantBounds(source);

            if (bounds == Rect.Empty)
            {
                var size = new Size(source.Width, source.Height);
                bounds = new Rect(size);
                source.Measure(size);
                source.Arrange(bounds);
            }


            var rtb = new RenderTargetBitmap((int)(bounds.Width * dpiX / 96.0),
                                             (int)(bounds.Height * dpiY / 96.0),
                                             dpiX,
                                             dpiY,
                                             PixelFormats.Pbgra32);

            rtb.Render(source);

            return rtb;
        }

        public static void AddRange<T>(this BindingList<T> list, IEnumerable<T> items)
        {
            list.RaiseListChangedEvents = false;
            foreach (var item in items)
            {
                list.Add(item);
            }
            list.RaiseListChangedEvents = true;
        }

    }
}
