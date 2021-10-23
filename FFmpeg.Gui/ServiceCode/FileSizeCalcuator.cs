//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.ServiceCode
{
    internal static class FileSizeCalcuator
    {
        public static long CalculateFileSizes(double videobitrate, double audiobitrate, double length)
        {
            double videobytes = (videobitrate * 1000) / 8;
            double audiobytes = (audiobitrate * 1000) / 8;
            return (long)((length * videobytes) + (length * audiobytes));
        }
    }
}
