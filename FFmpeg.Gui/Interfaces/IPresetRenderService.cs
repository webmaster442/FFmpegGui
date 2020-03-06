using FFmpeg.Gui.Domain;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetRenderService
    {
        void RenderPreset(IRenderPanel target, Preset preset);
    }
}
