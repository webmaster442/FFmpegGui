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
                    TargetExtension = "m4a",
                    Controllers = new List<ControlBase>()
                };
            }
        }
    }
}
