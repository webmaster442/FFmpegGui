using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel: MvxViewModel
    {
        public FileSelectorViewModel FileSelectorVM { get; set; }

        public MainViewModel()
        {
            FileSelectorVM = new FileSelectorViewModel();
        }

    }
}
