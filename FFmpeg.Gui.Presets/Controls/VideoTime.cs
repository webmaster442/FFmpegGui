//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;

namespace FFmpeg.Gui.Presets.Controls
{
    public record VideoTime : ControlBase
    {
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
    }
}
