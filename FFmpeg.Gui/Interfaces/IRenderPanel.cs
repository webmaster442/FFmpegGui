using System.Windows;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IRenderPanel
    {
        void Render(FrameworkElement element);
        FrameworkElement GetElement(string name);
    }
}
