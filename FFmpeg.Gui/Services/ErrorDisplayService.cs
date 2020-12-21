//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Presets;
using FFmpeg.Gui.Properties;
using System.Collections.Generic;
using System.IO;

namespace FFmpeg.Gui.Services
{
    internal class ErrorDisplayService: IErrorDisplayService
    {
        public IEnumerable<string> GetErrors(List<string>? files, Preset? preset, string outDirectory, string ffmpegPath, bool presetErrors)
        {
            if (files == null || files.Count < 1)
                yield return Resources.Error_NoInputFiles;

            if (preset == null)
                yield return Resources.Error_Preset;

            if (presetErrors)
                yield return Resources.Error_Preset_InvalidState;

            if (!File.Exists(ffmpegPath))
                yield return Resources.Error_FFmpeg;

            if (!Directory.Exists(outDirectory))
                yield return Resources.Error_OutDirectory;
        }
    }
}
