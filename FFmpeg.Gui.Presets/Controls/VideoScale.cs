//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Presets.Controls
{
    public record VideoScale : ControlBase
    {
        public int Width { get; init; }
        public int Height { get; init; }
    }
}
