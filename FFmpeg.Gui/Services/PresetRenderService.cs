using FFmpeg.Gui.Controls;
using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FFmpeg.Gui.Services
{
    /// <summary>
    /// Renders a preset to UI Controls
    /// </summary>
    internal class PresetRenderService: IPresetRenderService
    {
        private Thickness ControlMargin { get; }

        public PresetRenderService()
        {
            ControlMargin = new Thickness(10, 2, 10, 2); 
        }

        public void RenderPreset(IRenderPanel target, Preset preset)
        {
            foreach (var controller in preset.Controllers)
            {
                FrameworkElement rendered = null;
                switch (controller)
                {
                    case BitrateSlider bitrateSlider:
                        rendered = RenderBitrateSlider(bitrateSlider);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                target.Render(RenderLabel(controller.Label));
                target.Render(rendered);
            }
        }

        private TextBlock RenderLabel(string text)
        {
            var result = new TextBlock();
            result.Text = text;
            return result;
        }

        private Control RenderBitrateSlider(BitrateSlider bitrateSlider)
        {
            var slider = new SliderWithValueText
            {
                Minimum = bitrateSlider.Minimum,
                Maximum = bitrateSlider.Maximum,
                Value = bitrateSlider.Value,
                Name = bitrateSlider.Name,
                Margin = ControlMargin,
                ValueUnit = bitrateSlider.Unit,
            };
            if (bitrateSlider.PresetValues?.Length > 0)
            {
                slider.IsSnapToTickEnabled = true;
                slider.Ticks = new DoubleCollection(bitrateSlider.PresetValues.Select(x => (double)x));
            }

            return slider;
        }
    }
}
