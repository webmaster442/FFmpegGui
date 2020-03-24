//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain;
using System.Collections.Generic;

namespace FFmpeg.Gui.Presets
{
    internal static partial class FFmpegPresets
    {
        public static Preset NvH265
        {
            get
            {
                return new Preset
                {
                    Name = "Video, Nvidia H.265",
                    Description = "Convert video to Nvidia h.265 encoded, Aduio copied, container: mkv",
                    ArgumentCollection = new List<string>
                    {
                        "-vsync 0",
                        "-hwaccel nvdec",
                        "-i",
                        "%source%",
                        "-c:v hevc_nvenc",
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
