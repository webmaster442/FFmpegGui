//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets.Controls;
using System.Collections.Generic;
using System.Linq;

namespace FFmpeg.Gui.Presets
{
    public static partial class FFmpegPresets
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
                    Controllers = new List<ControlBase>
                    {
                        new Slider
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
