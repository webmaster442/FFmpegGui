//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Presets;
using System.Collections.Generic;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetBuilderService
    {
        string Build(
            IRenderPanel source,
            Preset? preset,
            IList<string> files,
            string OutputDirectory,
            string ffmpeg,
            FileHandlingMode fileHandlingMode);
    }
}
