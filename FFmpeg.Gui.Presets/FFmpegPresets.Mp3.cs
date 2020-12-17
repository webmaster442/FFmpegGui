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
        public static Preset Mp3
        {
            get
            {
                return new Preset
                {
                    Name = "Audio, Mp3",
                    Description = "Convert audio to Mp3 format",
                    ArgumentCollection = new List<string>
                    {
                        "-i",
                        "%source%",
                        "-vn",
                        "-c:a libmp3lame",
                        "-b:a",
                        "{AudioBitrate}k",
                        "%target%"
                    },
                    TargetExtension = "mp3",
                    Controllers = new List<ControlBase>
                    {
                        new Slider
                        {
                            Label = "Audio Bitrate",
                            Maximum = 320,
                            Minimum = 8,
                            Name = "AudioBitrate",
                            Unit = "kbit",
                            Value = 256,
                            PresetValues = new int[] { 8, 16, 24, 32, 40, 48, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 }
                        }
                    }
                };
            }
        }
    }
}
