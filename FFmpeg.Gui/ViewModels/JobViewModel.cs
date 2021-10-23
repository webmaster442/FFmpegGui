//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Infrastructure;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace FFmpeg.Gui.ViewModels
{
    internal class JobViewModel : MvxViewModel
    {
        private readonly SessionViewModel _session;
        private readonly IPresetBuilderService _presetBuilderService;
        private readonly IDialogService _dialogService;
        private string _fFmpegPath;
        private string _outputPath;
        private readonly IErrorDisplayService _errorDisplayService;
        private bool _errorsVisible;
        private FileHandlingMode _fileHandlingMode;

        public IRenderPanel? RenderTarget { get; set; }

        public ObservableCollectionExt<string> Errors { get; set; }

        public FileHandlingMode FileHandlingMode
        {
            get { return _fileHandlingMode; }
            set
            {
                SetProperty(ref _fileHandlingMode, value);
                Settings.Default.FileHandlingMode = (int)value;
                Settings.Default.Save();
            }
        }

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
            _fFmpegPath = string.Empty;
            _outputPath = string.Empty;

            _presetBuilderService = presetBuilderService;
            _dialogService = dialogService;
            _errorDisplayService = errorDisplayService;
            _session = session;
            Errors = new ObservableCollectionExt<string>();
            _session.PropertyChanged += _session_PropertyChanged;
            FFmpegPath = Settings.Default.FFmpegPath;
            OutputPath = Settings.Default.OutputPath;
            FileHandlingMode = (FileHandlingMode)Settings.Default.FileHandlingMode;
            PreviewCommand = new MvxCommand(OnPreview);
            SaveCommand = new MvxCommand(OnSave);
            ExecuteCommand = new MvxCommand(OnExecute);
            BrowseFFmpegCommand = new MvxCommand(OnBrowseFFmpeg);
            BrowseOutputFolderCommand = new MvxCommand(OnBrowseOutput);
        }

        private void _session_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            TriggerErrorDisplayCall();
        }

        public void TriggerErrorDisplayCall()
        {
            Errors.Clear();
            var result = _errorDisplayService.GetErrors(_session.InputFiles,
                                                        _session.CurrentPreset,
                                                        OutputPath,
                                                        FFmpegPath,
                                                        RenderTarget?.IsValid ?? true);
            Errors.AddRange(result);
            ErrorsVisible = Errors.Count > 0;
        }

        private string PrepareScript()
        {
            if (RenderTarget == null)
                throw new InvalidOperationException(nameof(RenderTarget));

            string content = _presetBuilderService.Build(RenderTarget,
                                                         _session.CurrentPreset,
                                                         _session.InputFiles,
                                                         OutputPath,
                                                         FFmpegPath,
                                                         FileHandlingMode);
            return content;
        }

        private static void RunScript(string fn)
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "powershell.exe";
            p.StartInfo.Arguments = $"-ExecutionPolicy Bypass -File {fn}";
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
            _dialogService.ShowTextPreview(script, "Script Preview");
        }

        private void OnExecute()
        {
            try
            {
                string fn = Path.ChangeExtension(Path.GetTempFileName(), ".ps1");

                File.WriteAllText(fn, PrepareScript(), Encoding.Default);
                RunScript(fn);
            }
            catch (Exception ex) when (ex is IOException || ex is Win32Exception)
            {
                _dialogService.ShowError(Resources.Error_Execute);
            }
        }

        private void OnSave()
        {
            string filter = "poweshell script|*.ps1";

            if (_dialogService.ShowSaveFileDialog(filter, out string file))
            {
                try
                {
                    File.WriteAllText(file, PrepareScript(), Encoding.Default);
                }
                catch (IOException)
                {
                    _dialogService.ShowError(Resources.Error_Save);
                }
            }

        }
    }
}
