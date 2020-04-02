//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace FFmpeg.Gui.Domain.Cd
{
    [StructLayout(LayoutKind.Sequential)]
    internal class TrackDataList
    {
        public const int MAXIMUM_NUMBER_TRACKS = 100;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMUM_NUMBER_TRACKS * 8)]
        private byte[] Data;

        public TrackData this[int Index]
        {
            get
            {
                if ((Index < 0) | (Index >= MAXIMUM_NUMBER_TRACKS))
                {
                    throw new IndexOutOfRangeException();
                }
                TrackData res;
                GCHandle handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
                try
                {
                    IntPtr buffer = handle.AddrOfPinnedObject();
                    buffer = (IntPtr)(buffer.ToInt64() + (Index * Marshal.SizeOf(typeof(TrackData))));
                    res = (TrackData)Marshal.PtrToStructure(buffer, typeof(TrackData));
                }
                finally
                {
                    handle.Free();
                }
                return res;
            }
        }

        public TrackDataList()
        {
            Data = new byte[MAXIMUM_NUMBER_TRACKS * Marshal.SizeOf(typeof(TrackData))];
        }
    }
}
