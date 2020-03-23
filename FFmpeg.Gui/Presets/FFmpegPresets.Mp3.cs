using FFmpeg.Gui.Domain;
using System.Collections.Generic;

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
                    ArgumentCollection = new List<string>
                    {
                        "-i",
                        "%source%",
                        "-vn",
                        "-c:a libmp3lame",
                        "-b:a",
                        "{AudioBitrate}k",
                        "%target%"
                    },
                    TargetExtension = "mp3",
                    Controllers = new List<PresetControl>
                    {
                        new BitrateSliderControl
                        {
                            Label = "Audio Bitrate",
                            Maximum = 320,
                            Minimum = 8,
                            Name = "AudioBitrate",
                            Unit = "kbit",
                            Value = 256,
                            PresetValues = new int[] { 8, 16, 24, 32, 40, 48, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 }
                        }
                    }
                };
            }
        }
    }
}
