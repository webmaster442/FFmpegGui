//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Presets.Controls
{
    public record Slider : ControlBase
    {
        public int Minimum { get; init; }
        public int Maximum { get; init; }
        public int Value { get; init; }
        public int[] PresetValues { get; init; }
        public string Unit { get; init; }

        public Slider()
        {
            PresetValues = System.Array.Empty<int>();
            Unit = string.Empty;
        }
    }
}
