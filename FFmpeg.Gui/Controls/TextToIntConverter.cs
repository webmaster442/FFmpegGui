using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FFmpeg.Gui.Controls
{
    internal class TextToIntConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (int.TryParse(str, out int parsed))
                    return parsed;
                else
                    return -1;
            }
            return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
