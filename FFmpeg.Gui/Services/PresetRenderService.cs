using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace FFmpeg.Gui.Services
{
    /// <summary>
    /// Renders a preset to UI Controls
    /// </summary>
    internal class PresetRenderService: IPresetRenderService
    {
        public void RenderPreset(IRenderPanel target, Preset preset)
        {
            foreach (var controller in preset.Controllers)
            {
                Control rendered = null;
                switch (controller)
                {
                    case BitrateSlider bitrateSlider:
                        rendered = RenderBitrateSlider(bitrateSlider);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                target.Render(rendered);
            }
        }

        private Control RenderBitrateSlider(BitrateSlider bitrateSlider)
        {
            var slider = new Slider
            {
                Minimum = bitrateSlider.Minimum,
                Maximum = bitrateSlider.Maximum,
                Value = bitrateSlider.Value,
                Name = bitrateSlider.Name,
            };
            if (bitrateSlider.PresetValues?.Length > 0)
            {
                slider.IsSnapToTickEnabled = true;
                slider.Ticks = new DoubleCollection(bitrateSlider.PresetValues.Cast<double>());
            }

            return slider;
        }
    }
}
