//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;

namespace FFmpeg.Gui.Controls
{
    internal class ToolPopup : Control
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ToolPopup), new PropertyMetadata(string.Empty));


        public object? Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ToolPopup), new PropertyMetadata(null));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_CLOSE") is Button closeButton)
            {
                closeButton.Click += CloseButton_Click;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            if (Content is IDisposable disposeable)
            {
                disposeable.Dispose();
            }
            Content = null;
        }
    }
}
