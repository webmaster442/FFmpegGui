using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel: MvxViewModel
    {
        public FileSelectorViewModel FileSelectorVM { get; }
        public PresetSelectorViewModel PresetSelectorVM { get; }
        public JobViewModel JobVM { get; }
        public Session Session { get; }

        public MainViewModel(IDialogService dialogService,
                             IPresetReaderService presetReaderService,
                             IPresetRenderService presetRenderService,
                             IPresetBuilderService presetBuilderService)
        {
            Session = new Session();
            FileSelectorVM = new FileSelectorViewModel(Session, dialogService);
            PresetSelectorVM = new PresetSelectorViewModel(Session, presetReaderService, presetRenderService);
            JobVM = new JobViewModel(Session, presetBuilderService, dialogService);
        }

    }
}
