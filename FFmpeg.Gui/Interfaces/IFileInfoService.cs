//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.Interfaces
{
    internal interface IFileInfoService
    {
        string RunFFmpeg(string ffmpegPath, string file);
    }
}
