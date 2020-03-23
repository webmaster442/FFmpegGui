using FFmpeg.Gui.Domain;
using System.Collections.Generic;

namespace FFmpeg.Gui.Presets
{
    internal static partial class FFmpegPresets
    {
        public static Preset NvH264
        {
            get
            {
                return new Preset
                {
                    Name = "Video, Nvidia H.264",
                    Description = "Convert video to Nvidia h.264 encoded, Aduio copied, container: mkv",
                    ArgumentCollection = new List<string>
                    {
                        "-vsync 0",
                        "-hwaccel nvdec",
                        "-i",
                        "%source%",
                        "-c:v h264_nvenc",
                        "-preset slow",
                        "-vf scale={Scale}",
                        "-c:a copy",
                        "%target%"
                    },
                    TargetExtension = "mkv",
                    Controllers = new List<PresetControl>
                    {
                        new VideoScaleControl
                        {
                            Name = "Scale",
                            Label = "Video Scale (-1, -1 : No resize)",
                            Width = -1,
                            Height = -1,
                        },
                    }
                };
            }
        }
    }
}
