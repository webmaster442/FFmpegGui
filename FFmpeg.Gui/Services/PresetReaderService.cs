using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Presets;
using FFmpeg.Gui.Properties;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FFmpeg.Gui.Services
{
    internal class PresetReaderService: IPresetReaderService
    {
        private readonly IDialogService _dialogService;

        public PresetReaderService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public IReadOnlyList<Preset> GetPresets()
        {
            try
            {
                var presetProperties = typeof(FFmpegPresets).GetProperties(BindingFlags.Public | BindingFlags.Static);

                var result = new List<Preset>(presetProperties.Length);

                foreach (var presetProperty in presetProperties)
                {
                    if (presetProperty.PropertyType == typeof(Preset))
                    {
                        result.Add((Preset)presetProperty.GetValue(null));
                    }
                }

                return result;
            }
            catch (Exception)
            {
                _dialogService.ShowError(Resources.Error_PresetXMLSerialize);
                return new List<Preset>();
            }
        }
    }
}
