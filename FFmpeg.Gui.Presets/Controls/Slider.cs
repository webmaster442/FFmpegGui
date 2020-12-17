namespace FFmpeg.Gui.Presets.Controls
{
    public class Slider: ControlBase
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int Value { get; set; }
        public int[] PresetValues { get; set; }
        public string Unit { get; set; }

        public Slider()
        {
            PresetValues = new int[0];
            Unit = string.Empty;
        }
    }
}
