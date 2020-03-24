//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Interfaces
{
    interface ITool
    {
        string Title { get; }
        void ConstructAndAssociateViewModel();
    }
}
