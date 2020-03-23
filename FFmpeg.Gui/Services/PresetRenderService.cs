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
            target.ClearItems();
            foreach (var controller in preset.Controllers)
            {
                FrameworkElement rendered = null;
                switch (controller)
                {
                    case BitrateSliderControl bitrateSlider:
                        rendered = RenderBitrateSlider(bitrateSlider);
                        break;
                    case VideoScaleControl videoScale:
                        rendered = RenderVideoScale(videoScale);
                        break;
                    case VideoTimeControl videoTime:
                        rendered = RenderVideoTime(videoTime);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                target.Render(RenderLabel(controller.Label));
                target.Render(rendered);
            }
        }

        private Control RenderVideoTime(VideoTimeControl videoTime)
        {
            var input = new TimeSpanInput
            {
                Name = videoTime.Name,
            };
            input.ViewModel.StartTime = videoTime.StartTime;
            input.ViewModel.EndTime = videoTime.EndTime;
            return input;
        }

        private TextBlock RenderLabel(string text)
        {
            var result = new TextBlock();
            result.Text = text;
            return result;
        }

        private Control RenderVideoScale(VideoScaleControl videoScale)
        {
            var scaler = new VideoScaleInput
            {
                VideoWidth = videoScale.Width,
                VideoHeight = videoScale.Height,
                Margin = ControlMargin,
                Name = videoScale.Name,
            };
            return scaler;
        }


        private Control RenderBitrateSlider(BitrateSliderControl bitrateSlider)
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
