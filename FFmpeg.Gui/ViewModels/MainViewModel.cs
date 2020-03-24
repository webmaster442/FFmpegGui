//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel: MvxViewModel
    {
        private int _tabIndex;
        private bool _toolFlyoutOpen;

        public FileSelectorViewModel FileSelectorVM { get; }
        public PresetSelectorViewModel PresetSelectorVM { get; }
        public JobViewModel JobVM { get; }
        public SessionViewModel Session { get; }
        public ToolsViewModel ToolsVM { get; }

        public MvxCommand GetFFmpegCommand { get; }
        public MvxCommand OpenCloseToolFlyoutCommand { get; }

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
                             IToolService toolService)
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

            ToolsVM = new ToolsViewModel(toolService);

            GetFFmpegCommand = new MvxCommand(OnGetFFmpeg);
            OpenCloseToolFlyoutCommand = new MvxCommand(OnOpenCloseFlyout);
        }

        private void OnGetFFmpeg()
        {
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "https://ffmpeg.zeranoe.com/builds/";
                process.Start();
            }
        }

        private void OnOpenCloseFlyout()
        {
            ToolFlyoutOpen = !ToolFlyoutOpen;
        }

    }
}
