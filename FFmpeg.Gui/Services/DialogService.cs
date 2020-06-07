//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Views;
using System.Windows;

namespace FFmpeg.Gui.Services
{
    public class DialogService : IDialogService
    {
        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string infoMessage)
        {
            MessageBox.Show(infoMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public bool ShowFileSelector(bool multiSelect, string filter, out string[] files)
        {
            var openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Filter = filter;
            openFile.Multiselect = multiSelect;
            if (openFile.ShowDialog() == true)
            {
                files = openFile.FileNames;
                return true;
            }
            else
            {
                files = new string[0];
                return false;
            }
        }

        public bool ShowFolderSelect(out string path)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                    return true;
                }
                else
                {
                    path = string.Empty;
                    return false;
                }
            }
        }

        public bool ShowSaveFileDialog(string filter, out string path)
        {
            var saveFile = new Microsoft.Win32.SaveFileDialog();
            saveFile.Filter = filter;
            if (saveFile.ShowDialog() == true)
            {
                path = saveFile.FileName;
                return true;
            }
            else
            {
                path = string.Empty;
                return false;
            }
        }

        public void ShowScriptPreview(string script)
        {
            var dialog = new PreviewScriptDialog(script);
            dialog.ShowDialog();
        }
    }
}
