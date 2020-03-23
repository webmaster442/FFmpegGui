using FFmpeg.Gui.Properties;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FFmpeg.Gui.Controls
{
    internal class TimeSpanConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (TimeSpan.TryParse(str, culture, out TimeSpan ts))
                {
                    return ts;
                }
                else
                {
                    return new ValidationResult(Resources.Error_Timespan_IncorrectFormat);
                }
            }
            else
            {
                return new ValidationResult(Resources.Error_Timespan_IncorrectFormat);
            }
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
