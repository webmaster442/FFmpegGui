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
        public static Preset Wv
        {
            get
            {
                return new Preset
                {
                    Name = "Audio, WavPack",
                    Description = "Convert audio to WavPack format",
                    ArgumentCollection = new List<string>
                    {
                        "-i",
                        "%source%",
                        "-vn",
                        "-c:a wavpack",
                        "-compression_level",
                        "{CompressionLevel}",
                        "%target%"
                    },
                    TargetExtension = "wv",
                    Controllers = new List<ControlBase>
                    {
                        new Slider
                        {
                            Label = "Compression Level",
                            Maximum = 5,
                            Minimum = 1,
                            Name = "CompressionLevel",
                            Unit = "",
                            Value = 2,
                            PresetValues = Enumerable.Range(1, 5).ToArray()
                        }
                    }
                };
            }
        }
    }
}