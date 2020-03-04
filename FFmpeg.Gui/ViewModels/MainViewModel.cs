using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpeg.Gui.ViewModels
{
    internal class MainViewModel
    {
        public FileSelectorViewModel FileSelectorVM { get; set; }

        public MainViewModel()
        {
            FileSelectorVM = new FileSelectorViewModel();
        }

    }
}
