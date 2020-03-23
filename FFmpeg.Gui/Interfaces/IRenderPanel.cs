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
