using FFmpeg.Gui.Interfaces;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel: MvxViewModel
    {
        public FileSelectorViewModel FileSelectorVM { get; set; }

        public MainViewModel(IDialogService dialogService)
        {
            FileSelectorVM = new FileSelectorViewModel(dialogService);
        }

    }
}
