﻿using FFmpeg.Gui.Interfaces;
using System.Windows;

namespace FFmpeg.Gui.Services
{
    public class DialogService : IDialogService
    {
        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                files = null;
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
            else
            {
                path = null;
                return false;
            }
        }
    }
}
