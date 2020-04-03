//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using MvvmCross.ViewModels;
using System;

namespace FFmpeg.Gui.ViewModels.ListItems
{
    internal class CdItemViewModel : MvxViewModel
    {
        private bool _isSelected;

        public string Name { get; set; }
        public TimeSpan Length { get; set; }
        public long Size { get; set; }

        public int Track { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}
