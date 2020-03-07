using FFmpeg.Gui.Domain;
using MvvmCross.ViewModels;
using System.Collections.Generic;

namespace FFmpeg.Gui.ViewModels
{
    internal class SessionViewModel: MvxViewModel
    {
        private Preset _currentPreset;
        private List<string> _inputFiles;

        public Preset CurrentPreset
        {
            get { return _currentPreset; }
            set { SetProperty(ref _currentPreset, value); }
        }

        public List<string> InputFiles
        {
            get { return _inputFiles; }
            set { SetProperty(ref _inputFiles, value); }
        }

        public SessionViewModel()
        {
            InputFiles = new List<string>();
        }
    }
}
