using FFmpeg.Gui.Domain;
using System.Collections.Generic;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetBuilderService
    {
        string Build(IRenderPanel source, Preset preset, IList<string> files, string OutputDirectory);
    }
}
