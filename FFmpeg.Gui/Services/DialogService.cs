//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace FFmpeg.Gui.Services
{
    public class DialogService : IDialogService
    {
        public async void ShowError(string errorMessage)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                await mainWindow.ShowMessageAsync("Error", errorMessage, MessageDialogStyle.Affirmative);
            }
            else MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public async void ShowInfo(string infoMessage)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                await mainWindow.ShowMessageAsync("Information", infoMessage, MessageDialogStyle.Affirmative);
            }
            else MessageBox.Show(infoMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
            files = new string[0];
            return false;
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
                path = string.Empty;
                return false;
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
            path = string.Empty;
            return false;
        }

        public void ShowTextPreview(string script, string title)
        {
            var dialog = new TextViewDialog(script, title);
            dialog.ShowDialog();
        }

        public void ShowChangeLog()
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.ShowChangeLog();
            }
        }
    }
}
