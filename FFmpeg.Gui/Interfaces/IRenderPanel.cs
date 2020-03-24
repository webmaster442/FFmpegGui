//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Windows;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IRenderPanel
    {
        void ClearItems();
        void Render(FrameworkElement element);
        FrameworkElement GetElement(string name);
        bool HasErrors { get; }
    }
}
