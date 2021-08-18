//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFmpeg.Gui.Presets
{
    internal static class PresetBuilders
    {
        public static readonly ValueSelector SampleRateSelector = new ValueSelector
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

        public static readonly ValueSelector ChannelSelectorMulti = new ValueSelector
        {
            Label = "Audio channels",
            Name = "ChannelSelectorMulti",
            Options = new Dictionary<string, string>
            {
                { "No change", string.Empty },
                { "1", "-ac 1" },
                { "2", "-ac 2" },
                { "6 (5.1)", "-ac 6" },
                { "8 (7.1)", "-ac 8" },
            },
            SelectedOptionKey = "No change",
        };

        public static readonly ValueSelector ChannelSelectorSimple = new ValueSelector
        {
            Label = "Audio channels",
            Name = "ChannelSelectorSimple",
            Options = new Dictionary<string, string>
            {
                { "No change", string.Empty },
                { "1", "-ac 1" },
                { "2", "-ac 2" },
                { "6 (5.1)", "-ac 6" },
                { "8 (7.1)", "-ac 8" },
            },
            SelectedOptionKey = "No change",
        };

        private static string GetName(LoslessFormat format)
        {
            switch (format)
            {
                case LoslessFormat.alac:
                    return "Apple Alac";
                case LoslessFormat.pcm_f32le:
                    return "Wav, 32 bit float";
                case LoslessFormat.pcm_s16le:
                    return "Wav, 16 bit (cd compatible)";
                case LoslessFormat.pcm_s24le:
                    return "Wav, 24 bit";
                default:
                    throw new ArgumentException(nameof(format));
            }
        }

        public static Preset BuildAudioLosless(LoslessFormat format, string ext)
        {
            return new Preset
            {
                Category = "Audio - Losless",
                Name = GetName(format),
                Description = $"Convert audio to {GetName(format)} format",
                ArgumentCollection = new string[]
                {
                    "-i",
                    "%source%",
                    "-vn",
                    $"-c:a {format.ToString().ToLower()}",
                    "{SampleRate}",
                    "{ChannelSelectorMulti}",
                    "%target%"
                },
                TargetExtension = ext,
                Controllers = new ControlBase[]
                {
                    SampleRateSelector,
                    ChannelSelectorMulti,
                }
            };
        }

        public static Preset BuildAudioLosless(string format, string ext, int min, int max, int value)
        {
            return new Preset
            {
                Category = "Audio - Losless",
                Name = $"{format}",
                Description = $"Convert audio to {format} format",
                ArgumentCollection = new string[]
                {
                    "-i",
                    "%source%",
                    "-vn",
                    $"-c:a {format.ToLower()}",
                    "-compression_level",
                    "{CompressionLevel}",
                    "{SampleRate}",
                    "{ChannelSelectorMulti}",
                    "%target%"
                },
                TargetExtension = ext,
                Controllers = new ControlBase[]
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
                    ChannelSelectorMulti,
                }
            };
        }

        public static Preset BuildAudio(string format, string codec, string ext, int min, int max, int value)
        {
            return new Preset
            {
                Category = "Audio",
                Name = $"{format}",
                Description = $"Convert audio to {format} format with variable bitrate",
                ArgumentCollection = new string[]
                {
                    "-i",
                    "%source%",
                    "-vn",
                    $"-c:a {codec}",
                    "-q:a",
                    "{AudioQuality}",
                    "%target%"
                },
                TargetExtension = ext,
                Controllers = new ControlBase[]
                {
                    new Slider
                    {
                        Label = "Audio Quality (lower better)",
                        Maximum = max,
                        Minimum = min,
                        Name = "AudioQuality",
                        Unit = "",
                        Value = value,
                        PresetValues = Enumerable.Range(0, 9).ToArray(),
                    }
                }
            };
        }

        public static Preset BuildAudio(string format, string codec, string ext, int bitrate)
        {
            return new Preset
            {
                Category = "Audio",
                Name = $"{format}",
                Description = $"Convert audio to {format} format",
                ArgumentCollection = new string[]
                {
                    "-i",
                    "%source%",
                    "-vn",
                    $"-c:a {codec}",
                    "-b:a",
                    "{AudioBitrate}k",
                    "%target%"
                },
                TargetExtension = ext,
                Controllers = new ControlBase[]
                {
                    new Slider
                    {
                        Label = "Audio Bitrate",
                        Maximum = 320,
                        Minimum = 8,
                        Name = "AudioBitrate",
                        Unit = "kbit",
                        Value = bitrate,
                        PresetValues = new int[] { 8, 16, 24, 32, 40, 48, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 }
                    }
                }
            };
        }
    }
}
