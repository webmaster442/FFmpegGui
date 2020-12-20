//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets.Controls;
using System.Collections.Generic;

namespace FFmpeg.Gui.Presets
{
    public static partial class FFmpegPresets
    {
        public static Preset NvH265
        {
            get
            {
                return new Preset
                {
                    Name = "Video, Nvidia H.265",
                    Description = "Convert video to Nvidia h.265 encoded, Aduio copied, container: mkv",
                    ArgumentCollection = new string[]
                    {
                        "-vsync 0",
                        "-hwaccel nvdec",
                        "-i",
                        "%source%",
                        "-c:v hevc_nvenc",
                        "-preset slow",
                        "-vf scale={Scale}{FrameRate}",
                        "-c:a copy",
                        "%target%"
                    },
                    TargetExtension = "mkv",
                    Controllers = new ControlBase[]
                    {
                        new VideoScale
                        {
                            Name = "Scale",
                            Label = "Video Scale (-1, -1 : No resize)",
                            Width = -1,
                            Height = -1,
                        },
                        new ValueSelector
                        {
                            Name = "FrameRate",
                            Label = "Frame rate",
                            Options = new Dictionary<string, string>
                            {
                                { "No change", string.Empty },
                                { "60 fps", ", fps=fps=60" },
                                { "50 fps", ", fps=fps=50" },
                                { "30 fps", ", fps=fps=30" },
                                { "25 fps", ", fps=fps=25" },
                            },
                            SelectedOptionKey = "No change"
                        }
                    }
                };
            }
        }
    }
}
