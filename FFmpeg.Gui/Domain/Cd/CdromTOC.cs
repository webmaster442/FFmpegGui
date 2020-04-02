//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace FFmpeg.Gui.Domain.Cd
{
    [StructLayout(LayoutKind.Sequential)]
    internal class CdromTOC
    {
        public ushort Length;
        public byte FirstTrack = 0;
        public byte LastTrack = 0;

        public TrackDataList TrackData;

        public CdromTOC()
        {
            TrackData = new TrackDataList();
            Length = (ushort)Marshal.SizeOf(this);
        }
    }
}
