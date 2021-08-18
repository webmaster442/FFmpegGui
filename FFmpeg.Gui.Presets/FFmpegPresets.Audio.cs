//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets.Controls;

namespace FFmpeg.Gui.Presets
{
    public static partial class FFmpegPresets
    {
        public static Preset Flac => PresetBuilders.BuildAudioLosless("FLAC", "flac", 1, 12, 8);
        public static Preset WavPack => PresetBuilders.BuildAudioLosless("WavPack", "wv", 1, 5, 2);
        public static Preset Alac => PresetBuilders.BuildAudioLosless(LoslessFormat.alac, "m4a");
        public static Preset Wav16Bit => PresetBuilders.BuildAudioLosless(LoslessFormat.pcm_s16le, "wav");
        public static Preset Wav24Bit => PresetBuilders.BuildAudioLosless(LoslessFormat.pcm_s24le, "wav");
        public static Preset Wav32Bit => PresetBuilders.BuildAudioLosless(LoslessFormat.pcm_f32le, "wav");

        public static Preset Mp3 => PresetBuilders.BuildAudio("Mp3", "libmp3lame", "mp3", 256);
        public static Preset Mp3Vbr => PresetBuilders.BuildAudio("Mp3 (VBR)", "libmp3lame", "mp3", 0, 9, 2);
        public static Preset Aac => PresetBuilders.BuildAudio("AAC/M4A", "aac", "m4a", 160);

        public static Preset Ac3
        {
            get
            {
                return new Preset
                {
                    Name = "Ac3",
                    Category = "Audio",
                    Description = "Convert audio to AC3 (Dolby Digital) format",
                    TargetExtension = "ac3",
                    ArgumentCollection = new string[]
                    {
                        "-i",
                        "%source%",
                        "-vn",
                        $"-c:a ac3",
                        "-compression_level",
                        "{CompressionLevel}",
                        "{SampleRate}",
                        "{ChannelSelectorMulti}",
                        "%target%"
                    },
                    Controllers = new Controls.ControlBase[]
                    {
                        new Slider
                        {
                            Label = "Audio Bitrate",
                            Maximum = 640,
                            Minimum = 384,
                            Name = "AudioBitrate",
                            Unit = "kbit",
                            Value = 448,
                            PresetValues = new int[] { 384, 448, 504, 640 }
                        },
                        PresetBuilders.SampleRateSelector,
                        PresetBuilders.ChannelSelectorMulti,
                    }
                };
            }
        }
    }
}
