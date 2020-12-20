//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Windows;

namespace FFmpeg.Gui.Views
{
    /// <summary>
    /// Interaction logic for PreviewScriptDialog.xaml
    /// </summary>
    public partial class TextViewDialog : Window
    {
        public TextViewDialog(string content, string title)
        {
            InitializeComponent();
            ScriptText.Text = content;
            Title = title;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
