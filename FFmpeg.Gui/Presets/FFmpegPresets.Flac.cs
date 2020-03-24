//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FFmpeg.Gui.Presets
{
    internal static partial class FFmpegPresets
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
                    Controllers = new List<PresetControl>
                    {
                        new BitrateSliderControl
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
