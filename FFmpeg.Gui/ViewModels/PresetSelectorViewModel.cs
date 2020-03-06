using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Presets;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FFmpeg.Gui.ViewModels
{
    public class PresetSelectorViewModel
    {
        public ObservableCollectionExt<Preset> Presets { get; private set; }

        public PresetSelectorViewModel()
        {
        }
    }
}
