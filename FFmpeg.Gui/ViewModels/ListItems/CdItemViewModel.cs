//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;

namespace FFmpeg.Gui.ViewModels.ListItems
{
    internal class CdItemViewModel
    {
        public string Name { get; set; }
        public TimeSpan Length { get; set; }
        public long Size { get; set; }
    }
}
