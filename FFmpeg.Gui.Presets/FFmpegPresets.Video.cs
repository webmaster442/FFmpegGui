//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets.Controls;
using System;
using System.Collections.Generic;

namespace FFmpeg.Gui.Presets
{
    public static partial class FFmpegPresets
    {
        private static readonly ValueSelector FrameRates = new ValueSelector
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
        };

        private static readonly ValueSelector NVPresets = new ValueSelector
        {
            Name = "NVPresets",
            Label = "Preset",
            Options = new Dictionary<string, string>
            {
                { "default", "default" },
                { "HQ 2 passes (slow)", "slow" },
                { "HQ 1 passes (medium)", "medium" },
                { "HQ 1 passes (fast)", "fast" },
                { "High Quality", "hq" },
                { "fastest (lowest quality)", "p1" },
                { "faster (lower quality)", "p2" },
                { "fast (low quality)", "p3" },
                { "medium (default)", "p4" },
                { "slow (good quality)", "p5" },
                { "slower (better quality)", "p6" },
                { "slowest (best quality)", "p7" },
            },
            SelectedOptionKey = "HQ 2 passes (slow)"
        };


        private static readonly VideoScale Scale = new VideoScale
        {
            Name = "Scale",
            Label = "Video Scale (-1, -1 : No resize)",
            Width = -1,
            Height = -1,
        };

        public static Preset CutNoRecode
        {
            get
            {
                return new Preset
                {
                    Category = "Video",
                    Name = "Video, Cut video without reencode",
                    Description = "Cut a portion of video without reencoding. Output Container is MKV",
                    ArgumentCollection = new string[]
                    {
                        "-ss {VideoTime.StartTime}",
                        "-i",
                        "%source%",
                        "-t {VideoTime.EndTime}",
                        "-c:a copy",
                        "-c:v copy",
                        "%target%"
                    },
                    TargetExtension = "mkv",
                    Controllers = new ControlBase[]
                    {
                        new VideoTime
                        {
                            Name = "VideoTime",
                            Label = "If End time is negative, then the end is the video length",
                            StartTime = TimeSpan.FromSeconds(0),
                            EndTime = TimeSpan.FromSeconds(-1),
                        }
                    },
                };
            }
        }

        public static Preset NvH264
        {
            get
            {
                return new Preset
                {
                    Category = "Video",
                    Name = "Video, Nvidia H.264",
                    Description = "Convert video to Nvidia h.264 encoded, Aduio copied, container: mkv",
                    ArgumentCollection = new string[]
                    {
                        "-vsync 0",
                        "-hwaccel nvdec",
                        "-i",
                        "%source%",
                        "-c:v h264_nvenc",
                        "{NVPresets}",
                        "-vf scale={Scale}",
                        "-c:a copy",
                        "%target%"
                    },
                    TargetExtension = "mkv",
                    Controllers = new ControlBase[]
                    {
                        NVPresets,
                        Scale,
                        FrameRates,
                    }
                };
            }
        }

        public static Preset NvH265
        {
            get
            {
                return new Preset
                {
                    Category = "Video",
                    Name = "Video, Nvidia H.265",
                    Description = "Convert video to Nvidia h.265 encoded, Aduio copied, container: mkv",
                    ArgumentCollection = new string[]
                    {
                        "-vsync 0",
                        "-hwaccel nvdec",
                        "-i",
                        "%source%",
                        "-c:v hevc_nvenc",
                        "{NVPresets}",
                        "-vf scale={Scale}{FrameRate}",
                        "-c:a copy",
                        "%target%"
                    },
                    TargetExtension = "mkv",
                    Controllers = new ControlBase[]
                    {
                        NVPresets,
                        Scale,
                        FrameRates,
                    }
                };
            }
        }

        public static Preset DVDPal
        {
            get
            {
                return new Preset
                {
                    Category = "Video",
                    Name = "DVD Video (Pal)",
                    Description = "Convert video to PAL DVD compatible video",
                    ArgumentCollection = new string[]
                    {
                        "-i",
                        "%source%",
                        "-target pal-dvd",
                        "%target%"
                    },
                    TargetExtension = "mpg",
                    Controllers = Array.Empty<ControlBase>()
                };
            }
        }

        public static Preset DVDNtsc
        {
            get
            {
                return new Preset
                {
                    Category = "Video",
                    Name = "DVD Video (NTSC)",
                    Description = "Convert video to NTSC DVD compatible video",
                    ArgumentCollection = new string[]
                    {
                        "-i",
                        "%source%",
                        "-target ntsc-dvd",
                        "%target%"
                    },
                    TargetExtension = "mpg",
                    Controllers = Array.Empty<ControlBase>()
                };
            }
        }
    }
}
