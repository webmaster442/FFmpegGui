//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace FFmpeg.Gui.Domain.Cd
{
    [StructLayout(LayoutKind.Sequential)]
    internal class RawReadInfo
    {
        public long DiskOffset = 0;
        public uint SectorCount = 0;
        public TrackModeType TrackMode = TrackModeType.CDDA;
    }
}
