using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.ComponentModel;
using System.IO;

namespace FFmpeg.Gui.ViewModels
{
    internal class JobViewModel : MvxViewModel
    {
        private readonly SessionViewModel _session;
        private readonly IPresetBuilderService _presetBuilderService;
        private readonly IDialogService _dialogService;
        private string _fFmpegPath;
        private bool _outputCmd;
        private bool _outputPowershell;
        private string _outputPath;
        private readonly IErrorDisplayService _errorDisplayService;
        private bool _errorsVisible;

        public IRenderPanel RenderTarget { get; set; }

        public ObservableCollectionExt<string> Errors { get; set; }

        public bool ErrorsVisible
        {
            get { return _errorsVisible; }
            set { SetProperty(ref _errorsVisible, value); }
        }

        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                SetProperty(ref _outputPath, value);
                Settings.Default.OutputPath = value;
                Settings.Default.Save();
                TriggerErrorDisplayCall();
            }
        }

        public string FFmpegPath
        {
            get { return _fFmpegPath; }
            set
            {
                SetProperty(ref _fFmpegPath, value);
                Settings.Default.FFmpegPath = value;
                Settings.Default.Save();
                TriggerErrorDisplayCall();
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
        public MvxCommand BrowseFFmpegCommand { get; }
        public MvxCommand BrowseOutputFolderCommand { get; }

        public JobViewModel(SessionViewModel session,
                            IPresetBuilderService presetBuilderService,
                            IDialogService dialogService,
                            IErrorDisplayService errorDisplayService)
        {
            _presetBuilderService = presetBuilderService;
            _dialogService = dialogService;
            _errorDisplayService = errorDisplayService;
            _session = session;
            Errors = new ObservableCollectionExt<string>();
            _session.PropertyChanged += _session_PropertyChanged;
            FFmpegPath = Settings.Default.FFmpegPath;
            OutputPath = Settings.Default.OutputPath;
            PreviewCommand = new MvxCommand(OnPreview);
            SaveCommand = new MvxCommand(OnSave);
            ExecuteCommand = new MvxCommand(OnExecute);
            BrowseFFmpegCommand = new MvxCommand(OnBrowseFFmpeg);
            BrowseOutputFolderCommand = new MvxCommand(OnBrowseOutput);
            OutputCmd = Settings.Default.OutputCmd;
            OutputPowershell = Settings.Default.OutputPowershell;
        }

        private void _session_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TriggerErrorDisplayCall();
        }

        private void TriggerErrorDisplayCall()
        {
            Errors.Clear();
            var result = _errorDisplayService.GetErrors(_session.InputFiles, 
                                                        _session.CurrentPreset, 
                                                        OutputPath,
                                                        FFmpegPath);
            Errors.AddRange(result);
            ErrorsVisible = Errors.Count > 0;
        }

        private string PrepareScript()
        {
            if (RenderTarget == null)
                throw new InvalidOperationException(nameof(RenderTarget));

            JobOutputFormat outputFormat = JobOutputFormat.Bach;

            if (OutputPowershell)
                outputFormat = JobOutputFormat.Powershell;

            string header = _presetBuilderService.GetShellScriptHeader(outputFormat);

            string content = _presetBuilderService.Build(RenderTarget,
                                                         _session.CurrentPreset,
                                                         _session.InputFiles,
                                                         OutputPath,
                                                         FFmpegPath);
            if (OutputCmd)
                return header + content;
            else
                return content;
        }

        private void RunScript(string fn)
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.FileName = OutputCmd ? "cmd.exe" : "powershell.exe";
            p.StartInfo.Arguments = OutputCmd ? $"/c {fn}" : fn;
            p.Start();

        }

        private void OnBrowseOutput()
        {
            if (_dialogService.ShowFolderSelect(out string folder))
            {
                OutputPath = folder;
            }
        }

        private void OnBrowseFFmpeg()
        {
            if (_dialogService.ShowFileSelector(false, "ffmpeg|ffmpeg.exe", out string[] selected))
            {
                FFmpegPath = selected[0];
            }
        }

        private void OnPreview()
        {
            var script = PrepareScript();
            _dialogService.ShowScriptPreview(script);
        }

        private void OnExecute()
        {
            try
            {
                string fn = Path.GetTempFileName();
                using (var writer = File.CreateText(fn))
                {
                    writer.Write(PrepareScript());
                }
                RunScript(fn);
            }
            catch (Exception ex) when (ex is IOException || ex is Win32Exception)
            {
                _dialogService.ShowError(Resources.Error_Execute);
            }
        }

        private void OnSave()
        {
            string filter = "cmd file|*.cmd";
            if (OutputPowershell)
                filter = "poweshell script|*.ps";

            if (_dialogService.ShowSaveFileDialog(filter, out string file))
            {
                try
                {
                    using (var writer = File.CreateText(file))
                    {
                        writer.Write(PrepareScript());
                    }
                }
                catch (IOException)
                {
                    _dialogService.ShowError(Resources.Error_Save);
                }
            }

        }
    }
}
