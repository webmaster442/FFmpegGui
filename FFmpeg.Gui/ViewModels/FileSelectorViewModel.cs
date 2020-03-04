using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class FileSelectorViewModel: MvxViewModel
    {
        private string _selectedFile;

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

        public FileSelectorViewModel()
        {
            Files = new ObservableCollectionExt<string>();
            AddFilesCommand = new MvxCommand(OnAddFiles);
            ClearListCommand = new MvxCommand(OnClearList);
            RemoveSelectedCommand = new MvxCommand<string>(OnRemoveSelected, CanRemoveSelected);
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
            var openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == true)
            {
                Files.AddRange(openFile.FileNames);
            }
        }
    }
}
