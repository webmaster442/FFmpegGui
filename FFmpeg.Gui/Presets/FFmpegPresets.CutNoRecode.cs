//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain;
using System;
using System.Collections.Generic;

namespace FFmpeg.Gui.Presets
{
    internal static partial class FFmpegPresets
    {
        public static Preset CutNoRecode
        {
            get
            {
                return new Preset
                {
                    Name = "Video, Cut video without reencode",
                    Description = "Cut a portion of video without reencoding. Output Container is MKV",
                    ArgumentCollection = new List<string>
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
                    Controllers = new List<PresetControl>
                    {
                        new VideoTimeControl
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
    }
}