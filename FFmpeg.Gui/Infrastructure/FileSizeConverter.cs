//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FFmpeg.Gui.Infrastructure
{
    internal class FileSizeConverter : MarkupExtension, IValueConverter
    {
        private const long Tib = 1_099_511_627_776;
        private const long Gib = 1_073_741_824;
        private const long Mib = 1_048_576;
        private const long Kib = 1024;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long)
            {
                long size = (long)value;
                return FileSize(size);
            }
            return Binding.DoNothing;
        }

        private static string FileSize(long size)
        {
            if (size > Tib)
                return $"{(double)size / Tib:0.00} TiB";
            else if (size > Gib)
                return $"{(double)size / Gib:0.00} GiB";
            else if (size > Mib)
                return $"{(double)size / Mib:0.00} MiB";
            else if (size > Kib)
                return $"{(double)size / Kib:0.00} kiB";
            else
                return $"{size} byte";
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
