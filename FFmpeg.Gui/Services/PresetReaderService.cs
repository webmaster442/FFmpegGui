//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Presets;
using System.Collections.Generic;
using System.Reflection;

namespace FFmpeg.Gui.Services
{
    internal class PresetReaderService : IPresetReaderService
    {
        public IReadOnlyList<Preset> GetPresets()
        {
            var presetProperties = typeof(FFmpegPresets).GetProperties(BindingFlags.Public | BindingFlags.Static);

            var result = new List<Preset>(presetProperties.Length);

            foreach (var presetProperty in presetProperties)
            {
                if (presetProperty.PropertyType == typeof(Preset)
                    && presetProperty.GetValue(null) is Preset temp)
                {
                    result.Add(temp);
                }
            }

            return result;
        }
    }
}
