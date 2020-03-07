using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using FFmpeg.Gui.Views;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;

namespace FFmpeg.Gui.ViewModels
{
    internal class JobViewModel: MvxViewModel
    {
        private readonly Session _session;
        private readonly IPresetBuilderService _presetBuilderService;
        private readonly IDialogService _dialogService;
        private string _fFmpegPath;
        private bool _outputCmd;
        private bool _outputBash;
        private bool _outputPowershell;

        public IRenderPanel RenderTarget { get; set; }

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

        public JobViewModel(Session session,
                            IPresetBuilderService presetBuilderService,
                            IDialogService dialogService)
        {
            FFmpegPath = Settings.Default.FFmpegPath;
            _session = session;
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

        private string PrepareScript()
        {
            if (RenderTarget == null)
                throw new InvalidOperationException(nameof(RenderTarget));

            JobOutputFormat outputFormat = JobOutputFormat.Bach;
            if (OutputCmd)
                outputFormat = JobOutputFormat.Bach;
            else if (OutputBash)
                outputFormat = JobOutputFormat.Bash;
            else if (_outputPowershell)
                outputFormat = JobOutputFormat.Powershell;

           var header = _presetBuilderService.GetShellScriptHeader(outputFormat);

            //_presetBuilderService.Build(RenderTarget, _session.CurrentPreset, )

            return header;

        }

        private void OnPreview()
        {
            throw new NotImplementedException();
        }

        private void OnExecute()
        {
            throw new NotImplementedException();
        }

        private void OnSave()
        {
            throw new NotImplementedException();
        }
    }
}
