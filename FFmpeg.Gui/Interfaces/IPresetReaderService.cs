using FFmpeg.Gui.Domain;
using System.Collections.Generic;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetReaderService
    {
        IReadOnlyList<Preset> GetPresets();
    }
}
