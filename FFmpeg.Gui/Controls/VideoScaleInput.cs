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


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var widthBox = GetTemplateChild("PART_VideoWith") as TextBox;
            var heightBox = GetTemplateChild("PART_VideoHeight") as TextBox;
            widthBox.PreviewTextInput += RestrictTextInput;
            heightBox.PreviewTextInput += RestrictTextInput;
        }

        private void RestrictTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
    }
}
