//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Infrastructure;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.ViewModels.ListItems;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;

namespace FFmpeg.Gui.ViewModels
{
    internal class FileSelectorViewModel: MvxViewModel
    {
        private FileSelectorItemViewModel? _selectedFile;
        private readonly IDialogService _dialogService;
        private readonly SessionViewModel _session;

        public ObservableCollectionExt<FileSelectorItemViewModel> Files { get; set; }

        public MvxCommand AddFilesCommand { get; }
        public MvxCommand ClearListCommand { get; }
        public MvxCommand<FileSelectorItemViewModel> RemoveSelectedCommand { get; }
        public MvxCommand<string[]> FilesDragedinCommand { get; }

        public FileSelectorItemViewModel? SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                SetProperty(ref _selectedFile, value);
                RemoveSelectedCommand.RaiseCanExecuteChanged();
            }
        }

        public FileSelectorViewModel(SessionViewModel session, IDialogService dialogService)
        {
            _session = session;
            Files = new ObservableCollectionExt<FileSelectorItemViewModel>();
            Files.CollectionChanged += (s, e) => _session.InputFiles = Files.Select(f => f.FullPath).ToList();
            AddFilesCommand = new MvxCommand(OnAddFiles);
            ClearListCommand = new MvxCommand(OnClearList);
            RemoveSelectedCommand = new MvxCommand<FileSelectorItemViewModel>(OnRemoveSelected, CanRemoveSelected);
            FilesDragedinCommand = new MvxCommand<string[]>(OnFilesDraggedIn);
            this._dialogService = dialogService;
        }

        private void OnFilesDraggedIn(string[] obj)
        {
            var models = obj.Select(f => new FileSelectorItemViewModel(f));
            Files.AddRange(models);
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
    }
}
