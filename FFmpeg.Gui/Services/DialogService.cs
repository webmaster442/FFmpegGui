using FFmpeg.Gui.Interfaces;
using System.Windows;

namespace FFmpeg.Gui.Services
{
    public class DialogService : IDialogService
    {
        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowFileSelector(bool multiSelect, out string[] files)
        {
            var openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = multiSelect;
            if (openFile.ShowDialog() == true)
            {
                files = openFile.FileNames;
                return true;
            }
            else
            {
                files = null;
                return false;
            }
        }
    }
}
