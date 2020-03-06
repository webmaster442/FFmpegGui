using FFmpeg.Gui.Interfaces;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel: MvxViewModel
    {
        public FileSelectorViewModel FileSelectorVM { get; set; }
        public PresetSelectorViewModel PresetSelectorVM { get; set; }

        public MainViewModel(IDialogService dialogService,
                             IPresetReaderService presetReaderService,
                             IPresetRenderService presetRenderService)
        {
            FileSelectorVM = new FileSelectorViewModel(dialogService);
            PresetSelectorVM = new PresetSelectorViewModel(presetReaderService, presetRenderService);
        }

    }
}
