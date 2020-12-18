//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FFmpeg.Gui.Controls
{
    internal class OptionSelector : ComboBox
    {
        public Dictionary<string, string> Options
        {
            get { return (Dictionary<string, string>)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register("Options", typeof(Dictionary<string, string>), typeof(OptionSelector), new PropertyMetadata(null, OptionsChange));

        private static void OptionsChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is OptionSelector opt)
            {
                opt.ItemsSource = opt.Options.Keys;
            }
        }

        public string SelectedOptionValue
        {
            get
            {
                if (SelectedItem is string key)
                {
                    return Options[key];
                }
                return string.Empty;
            }
        }
    }
}
