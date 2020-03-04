namespace FFmpeg.Gui.Interfaces
{
    internal interface IDialogService
    {
        void ShowError(string errorMessage);
        bool ShowFileSelector(bool multiSelect, out string[] files);
    }
}
