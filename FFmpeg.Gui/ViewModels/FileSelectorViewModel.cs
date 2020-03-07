using FFmpeg.Gui.Domain;
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
        private readonly Session _session;

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

        public FileSelectorViewModel(Session session, IDialogService dialogService)
        {
            _session = session;
            Files = new ObservableCollectionExt<string>();
            Files.CollectionChanged += (s, e) => _session.InputFiles = Files.ToList();
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
