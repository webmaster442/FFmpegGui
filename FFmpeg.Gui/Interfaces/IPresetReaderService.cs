//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Presets;
using System.Collections.Generic;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetReaderService
    {
        IReadOnlyList<Preset> GetPresets();
    }
}
