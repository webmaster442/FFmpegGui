//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
                FrameworkElement? rendered = null;
                rendered = controller switch
                {
                    SliderControl bitrateSlider => RenderBitrateSlider(bitrateSlider),
                    VideoScaleControl videoScale => RenderVideoScale(videoScale),
                    VideoTimeControl videoTime => RenderVideoTime(videoTime),
                    _ => throw new InvalidOperationException(),
                };
                target.Render(RenderLabel(controller.Label));
                target.Render(rendered);
            }
        }

        private TextBlock RenderLabel(string text)
        {
            return new TextBlock
            {
                Margin = ControlMargin,
                Text = text
            };
        }

        private Control RenderVideoTime(VideoTimeControl videoTime)
        {
            var input = new TimeSpanInput
            {
                Name = videoTime.Name,
                Margin = ControlMargin,
            };
            input.ViewModel.StartTime = videoTime.StartTime;
            input.ViewModel.EndTime = videoTime.EndTime;
            return input;
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


        private Control RenderBitrateSlider(SliderControl bitrateSlider)
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
