//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.IO;

namespace FFmpeg.Gui.ViewModels.ListItems
{
    internal class FileSelectorItemViewModel
    {
        public string FullPath { get; }

        public string Directory { get; }

        public long Size { get; }

        public FileSelectorItemViewModel(string path)
        {
            var f = new FileInfo(path);
            FullPath = path;
            Directory = f.DirectoryName ?? string.Empty;
            Size = f.Length;
        }
    }
}
