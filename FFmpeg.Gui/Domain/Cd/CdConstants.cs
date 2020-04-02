//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Domain.Cd
{
    public static class CdConstants
    {
        public const int NSECTORS = 13;
        public const int UNDERSAMPLING = 1;
        public const int CB_CDDASECTOR = 2368;
        public const int CB_QSUBCHANNEL = 16;
        public const int CB_CDROMSECTOR = 2048;
        public const int CB_AUDIO = (CB_CDDASECTOR - CB_QSUBCHANNEL);
    }
}
