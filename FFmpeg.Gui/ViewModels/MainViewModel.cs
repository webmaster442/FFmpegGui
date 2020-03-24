//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel: MvxViewModel
    {
        private int _tabIndex;

        public FileSelectorViewModel FileSelectorVM { get; }
        public PresetSelectorViewModel PresetSelectorVM { get; }
        public JobViewModel JobVM { get; }
        public SessionViewModel Session { get; }

        public MainViewModel(IDialogService dialogService,
                             IPresetReaderService presetReaderService,
                             IPresetRenderService presetRenderService,
                             IPresetBuilderService presetBuilderService,
                             IErrorDisplayService errorDisplayService)
        {
            Session = new SessionViewModel();

            FileSelectorVM = new FileSelectorViewModel(Session,
                                                       dialogService);

            PresetSelectorVM = new PresetSelectorViewModel(Session,
                                                           presetReaderService,
                                                           presetRenderService);
            JobVM = new JobViewModel(Session,
                                     presetBuilderService,
                                     dialogService,
                                     errorDisplayService);
        }

        public int TabIndex
        {
            get { return _tabIndex; }
            set
            {
                SetProperty(ref _tabIndex, value);
                if (value == 2)
                    JobVM.TriggerErrorDisplayCall();
            }
        }

    }
}
