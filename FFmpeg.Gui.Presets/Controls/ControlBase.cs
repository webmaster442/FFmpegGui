//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Presets.Controls
{
    public abstract class ControlBase
    {
        public string Name { get; set; }
        public string Label { get; set; }

        public ControlBase()
        {
            Name = string.Empty;
            Label = string.Empty;
        }
    }
}
