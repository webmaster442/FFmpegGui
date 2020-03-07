using FFmpeg.Gui.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class FileSelectorViewModel: MvxViewModel
    {
        private string _selectedFile;
        private readonly IDialogService _dialogService;

        public ObservableCollectionExt<string> Files { get; set; }

        public MvxCommand AddFilesCommand { get; }
        public MvxCommand ClearListCommand { get; }
        public MvxCommand<string> RemoveSelectedCommand { get; }

        public string SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                SetProperty(ref _selectedFile, value);
                RemoveSelectedCommand.RaiseCanExecuteChanged();
            }
        }

        public FileSelectorViewModel(IDialogService dialogService)
        {
            Files = new ObservableCollectionExt<string>();
            AddFilesCommand = new MvxCommand(OnAddFiles);
            ClearListCommand = new MvxCommand(OnClearList);
            RemoveSelectedCommand = new MvxCommand<string>(OnRemoveSelected, CanRemoveSelected);
            this._dialogService = dialogService;
        }

        private bool CanRemoveSelected(string arg)
        {
            return !string.IsNullOrEmpty(arg);
        }

        private void OnRemoveSelected(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
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
                Files.AddRange(files);
            }
        }
    }
}
