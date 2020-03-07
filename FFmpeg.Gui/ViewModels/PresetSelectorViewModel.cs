using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui.ViewModels
{
    internal class PresetSelectorViewModel: MvxViewModel
    {
        private readonly Session _session;
        private readonly IPresetRenderService _presetRenderService;
        private Preset _selected;
        private IRenderPanel _renderTarget;

        public ObservableCollectionExt<Preset> Presets { get; }

        public IRenderPanel RenderTarget
        {
            get { return _renderTarget; }
            set
            {
                if (value != null)
                {
                    _renderTarget = value;
                    _presetRenderService.RenderPreset(RenderTarget, Selected);
                }
            }
        }

        public Preset Selected
        {
            get { return _selected; }
            set
            {
                if (SetProperty(ref _selected, value)
                    && RenderTarget != null)
                {
                    _presetRenderService.RenderPreset(RenderTarget, value);
                    _session.CurrentPreset = value;
                }
            }
        }

        public PresetSelectorViewModel(Session session,
                                       IPresetReaderService presetReaderService,
                                       IPresetRenderService presetRenderService)
        {
            _session = session;
            _presetRenderService = presetRenderService;
            Presets = new ObservableCollectionExt<Preset>(presetReaderService.GetPresets());
            Selected = Presets[0];
            _session.CurrentPreset = Presets[0];
        }
    }
}
