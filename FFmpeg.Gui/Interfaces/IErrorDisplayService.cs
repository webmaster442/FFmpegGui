using FFmpeg.Gui.Domain;
using System.Collections.Generic;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IErrorDisplayService
    {
        IEnumerable<string> GetErrors(List<string> files, Preset preset, string outDirectory, string ffmpegPath, bool presetErrors);
    }
}
