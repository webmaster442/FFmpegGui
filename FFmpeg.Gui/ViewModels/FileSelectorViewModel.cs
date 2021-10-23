//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Infrastructure;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using FFmpeg.Gui.ViewModels.ListItems;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace FFmpeg.Gui.ViewModels
{
    internal class FileSelectorViewModel : MvxViewModel
    {
        private FileSelectorItemViewModel? _selectedFile;
        private readonly IDialogService _dialogService;
        private readonly IFileInfoService _infoService;
        private readonly SessionViewModel _session;

        public ObservableCollectionExt<FileSelectorItemViewModel> Files { get; set; }

        public MvxCommand AddFilesCommand { get; }
        public MvxCommand AddFolderCommand { get; }
        public MvxCommand ClearListCommand { get; }
        public MvxCommand<FileSelectorItemViewModel> RemoveSelectedCommand { get; }
        public MvxCommand<FileSelectorItemViewModel> InfoSelectedCommand { get; }

        public MvxCommand<string[]> FilesDragedinCommand { get; }

        public MvxCommand<int> SortCommand { get; }

        public FileSelectorItemViewModel? SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                SetProperty(ref _selectedFile, value);
                RemoveSelectedCommand.RaiseCanExecuteChanged();
                InfoSelectedCommand.RaiseCanExecuteChanged();
            }
        }

        public FileSelectorViewModel(SessionViewModel session, IDialogService dialogService, IFileInfoService infoService)
        {
            _session = session;
            _dialogService = dialogService;
            _infoService = infoService;
            Files = new ObservableCollectionExt<FileSelectorItemViewModel>();
            Files.CollectionChanged += UpdateSession;
            AddFilesCommand = new MvxCommand(OnAddFiles);
            ClearListCommand = new MvxCommand(OnClearList);
            RemoveSelectedCommand = new MvxCommand<FileSelectorItemViewModel>(OnRemoveSelected, CanRemoveSelected);
            FilesDragedinCommand = new MvxCommand<string[]>(OnFilesDraggedIn);
            AddFolderCommand = new MvxCommand(OnAddFolder);
            SortCommand = new MvxCommand<int>(OnSort);
            InfoSelectedCommand = new MvxCommand<FileSelectorItemViewModel>(OnInfo, CanGetInfo);
            AddArguments(Environment.GetCommandLineArgs());

        }

        private void AddArguments(string[] args)
        {
            foreach (var arg in args.Skip(1))
            {
                if (System.IO.File.Exists(arg))
                {
                    Files.Add(new FileSelectorItemViewModel(arg));
                }
                else if (System.IO.Directory.Exists(arg))
                {
                    string[] files = System.IO.Directory.GetFiles(arg);
                    var models = files.Select(f => new FileSelectorItemViewModel(f));
                    Files.AddRange(models);
                }

            }
        }

        private void UpdateSession(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _session.InputFiles = Files.Select(x => x.FullPath).ToList();
        }

        private void OnFilesDraggedIn(string[] obj)
        {
            var models = obj.Select(f => new FileSelectorItemViewModel(f));
            Files.AddRange(models);
        }

        private bool CanGetInfo(FileSelectorItemViewModel arg)
        {
            return arg != null
                   && !string.IsNullOrEmpty(Settings.Default.FFmpegPath)
                   && System.IO.File.Exists(Settings.Default.FFmpegPath);
        }

        private bool CanRemoveSelected(FileSelectorItemViewModel arg)
        {
            return arg != null;
        }

        private void OnRemoveSelected(FileSelectorItemViewModel obj)
        {
            if (obj != null)
                Files.Remove(obj);
        }

        private void OnClearList()
        {
            Files.Clear();
        }

        private void OnAddFiles()
        {
            if (_dialogService.ShowFileSelector(true, "all files|*.*", out string[] files))
            {
                var models = files.Select(f => new FileSelectorItemViewModel(f));
                Files.AddRange(models);
            }
        }

        private void OnAddFolder()
        {
            if (_dialogService.ShowFolderSelect(out string selectedFolder))
            {
                string[] files = System.IO.Directory.GetFiles(selectedFolder);
                var models = files.Select(f => new FileSelectorItemViewModel(f));
                Files.AddRange(models);
            }
        }

        private enum OrderMode
        {
            FileName = 0,
            Path = 1,
            Size = 2,
            Extension = 3,
            Reverse = 4
        }

        private List<FileSelectorItemViewModel> Sort(OrderMode orderMode)
        {
            return orderMode switch
            {
                OrderMode.FileName => Files.OrderBy(m => System.IO.Path.GetFileName(m.FullPath)).ToList(),
                OrderMode.Path => Files.OrderBy(m => m.FullPath).ToList(),
                OrderMode.Size => Files.OrderBy(m => m.Size).ToList(),
                OrderMode.Reverse => Files.Reverse().ToList(),
                OrderMode.Extension => Files.OrderBy(m => m.Extension).ToList(),
                _ => throw new InvalidOperationException($"Unknown {nameof(OrderMode)}"),
            };
        }

        private void OnSort(int obj)
        {
            var mode = (OrderMode)obj;
            List<FileSelectorItemViewModel>? result = Sort(mode);
            Files.Clear();
            Files.AddRange(result);
        }

        private void OnInfo(FileSelectorItemViewModel obj)
        {
            string output = _infoService.RunFFmpeg(Settings.Default.FFmpegPath, obj.FullPath);
            _dialogService.ShowTextPreview(output, "File info");
        }
    }
}
