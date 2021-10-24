//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel : MvxViewModel
    {
        private int _tabIndex;
        private bool _toolFlyoutOpen;
        private readonly IDialogService _dialogService;

        public FileSelectorViewModel FileSelectorVM { get; }
        public PresetSelectorViewModel PresetSelectorVM { get; }
        public JobViewModel JobVM { get; }
        public SessionViewModel Session { get; }
        public ToolsViewModel ToolsVM { get; }

        public MvxCommand GetFFmpegCommand { get; }
        public MvxCommand OpenCloseToolFlyoutCommand { get; }
        public MvxCommand ShowChangelogCommand { get; }

        public int TabIndex
        {
            get { return _tabIndex; }
            set
            {
                SetProperty(ref _tabIndex, value);
                if (value == 2)
                    JobVM.TriggerErrorDisplayCall();
                JobVM.ExecuteCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ToolFlyoutOpen
        {
            get { return _toolFlyoutOpen; }
            set { SetProperty(ref _toolFlyoutOpen, value); }
        }

        public MainViewModel(IDialogService dialogService,
                             IPresetReaderService presetReaderService,
                             IPresetRenderService presetRenderService,
                             IPresetBuilderService presetBuilderService,
                             IErrorDisplayService errorDisplayService,
                             IToolService toolService,
                             IFileInfoService fileInfoService)
        {
            _dialogService = dialogService;
            Session = new SessionViewModel();

            FileSelectorVM = new FileSelectorViewModel(Session,
                                                       dialogService,
                                                       fileInfoService);

            PresetSelectorVM = new PresetSelectorViewModel(Session,
                                                           presetReaderService,
                                                           presetRenderService);
            JobVM = new JobViewModel(Session,
                                     presetBuilderService,
                                     dialogService,
                                     errorDisplayService);

            ToolsVM = new ToolsViewModel(toolService);

            GetFFmpegCommand = new MvxCommand(OnGetFFmpeg);
            OpenCloseToolFlyoutCommand = new MvxCommand(OnOpenCloseFlyout);
            ShowChangelogCommand = new MvxCommand(OnShowChangeLog);

        }

        private void OnShowChangeLog()
        {
            _dialogService.ShowChangeLog();
        }

        private void OnGetFFmpeg()
        {
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "https://www.gyan.dev/ffmpeg/builds/";
                process.Start();
            }
        }

        private void OnOpenCloseFlyout()
        {
            ToolFlyoutOpen = !ToolFlyoutOpen;
        }

    }
}
