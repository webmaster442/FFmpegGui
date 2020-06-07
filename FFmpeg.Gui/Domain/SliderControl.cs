//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Domain
{
    public class SliderControl : PresetControl
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int Value { get; set; }
        public int[] PresetValues { get; set; }
        public string Unit { get; set; }

        public SliderControl()
        {
            PresetValues = new int[0];
            Unit = string.Empty;
        }
    }
}
