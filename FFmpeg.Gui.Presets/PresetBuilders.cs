//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets.Controls;
using System.Collections.Generic;
using System.Linq;

namespace FFmpeg.Gui.Presets
{
    internal static class PresetBuilders
    {
        public static ValueSelector SampleRateSelector = new ValueSelector
        {
            Label = "Sample rate",
            Name = "SampleRate",
            Options = new Dictionary<string, string>
            {
                { "No change", string.Empty },
                { "192 kHz", "-ar 192000" },
                { "96 kHz", "-ar 96000" },
                { "48 kHz", "-ar 48000" },
                { "44.1 kHz", "-ar 44100" },
                { "22.05 kHz", "-ar 22050" },
            },
            SelectedOptionKey = "No change",
        };

        public static Preset BuildAudioLosless(string format, string ext)
        {
            return new Preset
            {
                Name = $"Audio, {format}",
                Description = $"Convert audio to {format} format",
                ArgumentCollection = new List<string>
                {
                    "-i",
                    "%source%",
                    "-vn",
                    $"-c:a {format.ToLower()}",
                    "-compression_level",
                    "{CompressionLevel}",
                    "{SampleRate}",
                    "%target%"
                },
                TargetExtension = ext,
                Controllers = new List<ControlBase>
                {
                    SampleRateSelector,
                }
            };
        }

        public static Preset BuildAudioLosless(string format, string ext, int min, int max, int value)
        {
            return new Preset
            {
                Name = $"Audio, {format}",
                Description = $"Convert audio to {format} format",
                ArgumentCollection = new List<string>
                {
                    "-i",
                    "%source%",
                    "-vn",
                    $"-c:a {format.ToLower()}",
                    "-compression_level",
                    "{CompressionLevel}",
                    "%target%"
                },
                TargetExtension = ext,
                Controllers = new List<ControlBase>
                {
                    new Slider
                    {
                        Label = "Compression Level",
                        Maximum = max,
                        Minimum = min,
                        Name = "CompressionLevel",
                        Unit = "",
                        Value = value,
                        PresetValues = Enumerable.Range(min, max).ToArray()
                    },
                    SampleRateSelector,
                }
            };
        }
    }
}
