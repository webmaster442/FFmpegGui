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
        public static Preset Flac
        {
            get
            {
                return new Preset
                {
                    Name = "Audio, Flac",
                    Description = "Convert audio to Flac format",
                    ArgumentCollection = new List<string>
                    {
                        "-i",
                        "%source%",
                        "-vn",
                        "-c:a flac",
                        "-compression_level",
                        "{CompressionLevel}",
                        "%target%"
                    },
                    TargetExtension = "flac",
                    Controllers = new List<ControlBase>
                    {
                        new Slider
                        {
                            Label = "Compression Level",
                            Maximum = 12,
                            Minimum = 1,
                            Name = "CompressionLevel",
                            Unit = "",
                            Value = 8,
                            PresetValues = Enumerable.Range(1, 12).ToArray()
                        }
                    }
                };
            }
        }
    }
}
