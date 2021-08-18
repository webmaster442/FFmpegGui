//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Interfaces
{
    internal interface IDialogService
    {
        void ShowError(string errorMessage);
        bool ShowFileSelector(bool multiSelect, string filter, out string[] files);
        bool ShowSaveFileDialog(string filter, out string path);
        bool ShowFolderSelect(out string path);
        void ShowTextPreview(string script, string title);
        void ShowInfo(string infoMessage);
        void ShowChangeLog();
    }
}
