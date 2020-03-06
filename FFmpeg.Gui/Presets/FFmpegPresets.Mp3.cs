using FFmpeg.Gui.Domain;

namespace FFmpeg.Gui.Presets
{
    internal static partial class FFmpegPresets
    {
        public static Preset Mp3
        {
            get
            {
                return new Preset
                {
                    Name = "Audio, Mp3",
                    Description = "Convert audio to Mp3 format",
                    ArgumentCollection = new string[]
                    {

                    },
                    Controllers = new PresetControl[]
                    {
                        new BitrateSlider
                        {
                            Label = "Bitrate",
                            Maximum = 320,
                            Minimum = 128,
                            Name = "AudioBitrate",
                            Value = 128,
                        }
                    }
                };
            }
        }
    }
}
