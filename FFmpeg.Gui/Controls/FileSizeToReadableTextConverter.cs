//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FFmpeg.Gui.Controls
{
    internal class FileSizeToReadableTextConverter : MarkupExtension, IValueConverter
    {
        private const long Kib = 1024;
        private const long Mib = Kib * 1024;
        private const long Gib = Mib * 1024;
        private const long Tib = Gib * 1024;
        private const long Pib = Tib * 1024;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long input)
            {
                double bytes = input;

                if (bytes > Pib)
                    return $"~ {Math.Round(bytes / Pib, 3)} Pib";
                else if (bytes > Tib)
                    return $"~ {Math.Round(bytes / Tib, 3)} Tib";
                else if (bytes > Gib)
                    return $"~ {Math.Round(bytes / Gib, 3)} Gib";
                else if (bytes > Mib)
                    return $"~ {Math.Round(bytes / Mib, 3)} Mib";
                if (bytes > Kib)
                    return $"~ {Math.Round(bytes / Kib, 3)} Kib";
                else
                    return $"{bytes} bytes";
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
