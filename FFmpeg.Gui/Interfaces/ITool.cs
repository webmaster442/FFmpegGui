//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using MvvmCross.Platforms.Wpf.Views;

namespace FFmpeg.Gui.Interfaces
{
    interface ITool: IMvxWpfView
    {
        string Title { get; }
    }
}
