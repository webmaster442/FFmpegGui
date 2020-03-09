using FFmpeg.Gui.Domain;
using System.Collections.Generic;

namespace FFmpeg.Gui.Presets
{
    internal static partial class FFmpegPresets
    {
        public static Preset ALAC
        {
            get
            {
                return new Preset
                {
                    Name = "Audio, ALAC",
                    Description = "Convert audio to Apple lossless/M4A format",
                    ArgumentCollection = new List<string>
                    {
                        "-i",
                        "%source%",
                        "-vn",
                        "-c:a alac",
                        "%target%"
                    },
                    TargetExtension = "m4a"
                };
            }
        }
    }
}
