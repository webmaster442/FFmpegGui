namespace FFmpeg.Gui.Interfaces
{
    internal interface IDialogService
    {
        void ShowError(string errorMessage);
        bool ShowFileSelector(bool multiSelect, string filter, out string[] files);
        bool ShowSaveFileDialog(string filter, out string path);
    }
}
