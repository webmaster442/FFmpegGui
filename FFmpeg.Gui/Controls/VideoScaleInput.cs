//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Infrastructure;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FFmpeg.Gui.Controls
{
    public class VideoScaleInput: Control
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public int VideoWidth
        {
            get { return (int)GetValue(VideoWidthProperty); }
            set { SetValue(VideoWidthProperty, value); }
        }

        public static readonly DependencyProperty VideoWidthProperty =
            DependencyProperty.Register("VideoWidth", typeof(int), typeof(VideoScaleInput), new PropertyMetadata(-1));

        public int VideoHeight
        {
            get { return (int)GetValue(VideoHeightProperty); }
            set { SetValue(VideoHeightProperty, value); }
        }

        public static readonly DependencyProperty VideoHeightProperty =
            DependencyProperty.Register("VideoHeight", typeof(int), typeof(VideoScaleInput), new PropertyMetadata(-1));

        public bool IsMaxSize
        {
            get { return (bool)GetValue(IsMaxSizeProperty); }
            set { SetValue(IsMaxSizeProperty, value); }
        }

        public static readonly DependencyProperty IsMaxSizeProperty =
            DependencyProperty.Register("IsMaxSize", typeof(bool), typeof(VideoScaleInput), new PropertyMetadata(false));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_VideoWith") is TextBox widthBox)
            {
                widthBox.PreviewTextInput += RestrictTextInput;
            }
            if (GetTemplateChild("PART_VideoHeight") is TextBox heightBox)
            {
                heightBox.PreviewTextInput += RestrictTextInput;
            }
            if (GetTemplateChild("PART_ResolutionTemplates") is WrapPanel wrapPanel)
            {
                foreach (var button in wrapPanel.GetChildren<Button>())
                {
                    button.Click += SizeTemplateButtonClick;
                }
            }
        }

        private void SizeTemplateButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var size = (string)button.Tag;
                var sizes = size.Split("×");
                if (GetTemplateChild("PART_VideoWith") is TextBox widthBox)
                    widthBox.Text = sizes[0].Trim();
                if (GetTemplateChild("PART_VideoHeight") is TextBox heightBox)
                    heightBox.Text = sizes[1].Trim();
            }
        }

        private void RestrictTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
    }
}
