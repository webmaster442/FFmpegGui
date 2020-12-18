//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Presets
{
    public static partial class FFmpegPresets
    {
        public static Preset Flac => PresetBuilders.BuildAudioLosless("FLAC", "flac", 1, 12, 8);
        public static Preset WavPack => PresetBuilders.BuildAudioLosless("WavPack", "wv", 1, 5, 2);
        public static Preset Alac => PresetBuilders.BuildAudioLosless("ALAC", "m4a");
        public static Preset Wav => PresetBuilders.BuildAudioLosless("Wav", "wav");

        public static Preset Mp3 => PresetBuilders.BuildAudio("Mp3", "libmp3lame", "mp3", 256);
        public static Preset Mp3Vbr => PresetBuilders.BuildAudio("Mp3 (VBR)", "libmp3lame", "mp3", 0, 9, 2);
        public static Preset Aac => PresetBuilders.BuildAudio("AAC/M4A", "aac", "m4a", 160);
    }
}
