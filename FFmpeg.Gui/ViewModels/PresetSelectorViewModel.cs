using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class PresetSelectorViewModel: MvxViewModel
    {
        public ObservableCollectionExt<Preset> Presets { get; }

        public PresetSelectorViewModel(IPresetReaderService presetReaderService)
        {
            Presets = new ObservableCollectionExt<Preset>(presetReaderService.GetPresets());
        }
    }
}
