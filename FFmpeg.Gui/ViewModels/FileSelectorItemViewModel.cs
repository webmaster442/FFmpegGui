//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
