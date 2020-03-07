using System.Windows;

namespace FFmpeg.Gui.Views
{
    /// <summary>
    /// Interaction logic for PreviewScriptDialog.xaml
    /// </summary>
    public partial class PreviewScriptDialog : Window
    {
        public PreviewScriptDialog(string content)
        {
            InitializeComponent();
            ScriptText.Text = content;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
