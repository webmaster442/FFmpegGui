//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetRenderService
    {
        void RenderPreset(IRenderPanel target, Preset preset);
    }
}
