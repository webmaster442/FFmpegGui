using FFmpeg.Gui.Interfaces;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel: MvxViewModel
    {
        public FileSelectorViewModel FileSelectorVM { get; }
        public PresetSelectorViewModel PresetSelectorVM { get; }
        public JobViewModel JobVM { get; }

        public MainViewModel(IDialogService dialogService,
                             IPresetReaderService presetReaderService,
                             IPresetRenderService presetRenderService,
                             IPresetBuilderService presetBuilderService)
        {
            FileSelectorVM = new FileSelectorViewModel(dialogService);
            PresetSelectorVM = new PresetSelectorViewModel(presetReaderService, presetRenderService);
            JobVM = new JobViewModel(presetBuilderService, dialogService);
        }

    }
}
