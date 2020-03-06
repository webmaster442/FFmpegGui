namespace FFmpeg.Gui.Domain
{
    public class BitrateSlider: PresetControl
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int Value { get; set; }
        public int[] PresetValues { get; set; }
        public string Unit { get; set; }
    }
}
