using System;

namespace FFmpeg.Gui.Domain
{
    internal class VideoTimeControl: PresetControl
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
