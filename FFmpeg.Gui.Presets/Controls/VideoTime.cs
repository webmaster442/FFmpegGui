using System;

namespace FFmpeg.Gui.Presets.Controls
{
    public class VideoTime: ControlBase
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
