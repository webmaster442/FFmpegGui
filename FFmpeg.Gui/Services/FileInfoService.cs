//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using System.Diagnostics;

namespace FFmpeg.Gui.Services
{
    internal class FileInfoService : IFileInfoService
    {
        //ffmpeg -hide_banner -i "file.mp4"
        public string RunFFmpeg(string ffmpegPath, string file)
        {
            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = $"-hide_banner -i \"{file}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };
            p.Start();
            var output = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return output;
        }
    }
}
