namespace FFmpeg.Gui.ViewModels
{
    internal class FileSelectorItemViewModel
    {
        public string FullPath { get; }

        public string Directory { get; }

        public FileSelectorItemViewModel(string path)
        {
            FullPath = path;
            Directory = System.IO.Path.GetDirectoryName(path);
        }
    }
}
