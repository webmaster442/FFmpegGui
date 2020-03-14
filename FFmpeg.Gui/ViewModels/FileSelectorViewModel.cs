using FFmpeg.Gui.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Linq;

namespace FFmpeg.Gui.ViewModels
{
    internal class FileSelectorViewModel: MvxViewModel
    {
        private string _selectedFile;
        private readonly IDialogService _dialogService;
        private readonly SessionViewModel _session;

        public ObservableCollectionExt<string> Files { get; set; }

        public MvxCommand AddFilesCommand { get; }
        public MvxCommand ClearListCommand { get; }
        public MvxCommand<string> RemoveSelectedCommand { get; }
        public MvxCommand<string[]> FilesDragedinCommand { get; }

        public string SelectedFile
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
            Files = new ObservableCollectionExt<string>();
            Files.CollectionChanged += (s, e) => _session.InputFiles = Files.ToList();
            AddFilesCommand = new MvxCommand(OnAddFiles);
            ClearListCommand = new MvxCommand(OnClearList);
            RemoveSelectedCommand = new MvxCommand<string>(OnRemoveSelected, CanRemoveSelected);
            FilesDragedinCommand = new MvxCommand<string[]>(OnFilesDraggedIn);
            this._dialogService = dialogService;
        }

        private void OnFilesDraggedIn(string[] obj)
        {
            Files.AddRange(obj);
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
