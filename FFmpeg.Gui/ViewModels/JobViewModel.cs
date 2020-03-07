using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;

namespace FFmpeg.Gui.ViewModels
{
    internal class JobViewModel: MvxViewModel
    {
        private readonly IPresetBuilderService _presetBuilderService;
        private readonly IDialogService _dialogService;
        private string _fFmpegPath;
        private bool _outputCmd;
        private bool _outputBash;
        private bool _outputPowershell;

        public string FFmpegPath
        {
            get { return _fFmpegPath; }
            set
            {
                SetProperty(ref _fFmpegPath, value);
                Settings.Default.FFmpegPath = value;
                Settings.Default.Save();
            }
        }

        public bool OutputCmd
        {
            get { return _outputCmd; }
            set
            {
                SetProperty(ref _outputCmd, value);
                Settings.Default.OutputCmd = value;
                Settings.Default.Save();
            }
        }

        public bool OutputBash
        {
            get { return _outputBash; }
            set
            {
                SetProperty(ref _outputBash, value);
                Settings.Default.OutputBash = value;
                Settings.Default.Save();
            }
        }

        public bool OutputPowershell
        {
            get { return _outputPowershell; }
            set
            {
                SetProperty(ref _outputPowershell, value);
                Settings.Default.OutputPowershell = value;
                Settings.Default.Save();
            }
        }

        public MvxCommand PreviewCommand { get; }
        public MvxCommand SaveCommand { get; }
        public MvxCommand ExecuteCommand { get; }
        public MvxCommand BrowseCommand { get; }

        public JobViewModel(IPresetBuilderService presetBuilderService,
                            IDialogService dialogService)
        {
            FFmpegPath = Settings.Default.FFmpegPath;
            _presetBuilderService = presetBuilderService;
            _dialogService = dialogService;
            PreviewCommand = new MvxCommand(OnPreview);
            SaveCommand = new MvxCommand(OnSave);
            ExecuteCommand = new MvxCommand(OnExecute);
            BrowseCommand = new MvxCommand(OnBrowse);
            OutputCmd = Settings.Default.OutputCmd;
            OutputBash = Settings.Default.OutputBash;
            OutputPowershell = Settings.Default.OutputPowershell;
        }

        private void OnBrowse()
        {
            if (_dialogService.ShowFileSelector(false, "ffmpeg|ffmpeg.exe", out string[] selected))
            {
                FFmpegPath = selected[0];
            }
        }

        private void OnExecute()
        {
            throw new NotImplementedException();
        }

        private void OnSave()
        {
            throw new NotImplementedException();
        }

        private void OnPreview()
        {
            throw new NotImplementedException();
        }
    }
}
