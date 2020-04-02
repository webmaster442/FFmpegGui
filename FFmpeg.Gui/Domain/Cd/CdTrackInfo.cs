//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Domain.Cd
{
    public class CdTrackInfo
    {
        public bool IsAudio { get; set; }
        public double Length { get; set; }
        public uint Size { get; set; }
    }
}
