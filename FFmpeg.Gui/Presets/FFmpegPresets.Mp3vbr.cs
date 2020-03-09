using FFmpeg.Gui.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FFmpeg.Gui.Presets
{
    internal static partial class FFmpegPresets
    {
        public static Preset Mp3Vbr
        {
            get
            {
                return new Preset
                {
                    Name = "Audio, Mp3 (VBR)",
                    Description = "Convert audio to Mp3 format with variable bitrate",
                    ArgumentCollection = new List<string>
                    {
                        "-i",
                        "%source%",
                        "-vn",
                        "-c:a libmp3lame",
                        "-q:a",
                        "{AudioQuality}",
                        "%target%"
                    },
                    TargetExtension = "mp3",
                    Controllers = new List<PresetControl>
                    {
                        new BitrateSlider
                        {
                            Label = "Audio Quality (lower better)",
                            Maximum = 9,
                            Minimum = 0,
                            Name = "AudioQuality",
                            Unit = "",
                            Value = 2,
                            PresetValues = Enumerable.Range(0, 9).ToArray(),
                        }
                    }
                };
            }
        }
    }
}
