//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Controls;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Presets;
using FFmpeg.Gui.Presets.Controls;
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
    internal class PresetRenderService : IPresetRenderService
    {
        private Thickness ControlMargin { get; }
        private Thickness ControlMarginInner { get; }

        public PresetRenderService()
        {
            ControlMargin = new Thickness(10, 5, 10, 5);
            ControlMarginInner = new Thickness(18, 5, 10, 5);
        }

        public void RenderPreset(IRenderPanel target, Preset preset)
        {
            target.ClearItems();
            foreach (var controller in preset.Controllers)
            {
                FrameworkElement? rendered = null;
                rendered = controller switch
                {
                    Presets.Controls.Slider bitrateSlider => RenderBitrateSlider(bitrateSlider),
                    VideoScale videoScale => RenderVideoScale(videoScale),
                    VideoTime videoTime => RenderVideoTime(videoTime),
                    ValueSelector valueSelector => RenderValueSelector(valueSelector),
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
                Text = text + ":",
                FontWeight = FontWeights.Bold,
            };
        }

        private Control RenderValueSelector(ValueSelector valueSelector)
        {
            return new OptionSelector
            {
                Name = valueSelector.Name,
                Margin = ControlMarginInner,
                Options = valueSelector.Options,
                SelectedItem = valueSelector.SelectedOptionKey,
            };
        }

        private Control RenderVideoTime(VideoTime videoTime)
        {
            var input = new TimeSpanInput
            {
                Name = videoTime.Name,
                Margin = ControlMarginInner,
            };
            input.ViewModel.StartTime = videoTime.StartTime;
            input.ViewModel.EndTime = videoTime.EndTime;
            return input;
        }

        private Control RenderVideoScale(VideoScale videoScale)
        {
            var scaler = new VideoScaleInput
            {
                VideoWidth = videoScale.Width,
                VideoHeight = videoScale.Height,
                Margin = ControlMarginInner,
                Name = videoScale.Name,
            };
            return scaler;
        }


        private Control RenderBitrateSlider(Presets.Controls.Slider bitrateSlider)
        {
            var slider = new SliderWithValueText
            {
                Minimum = bitrateSlider.Minimum,
                Maximum = bitrateSlider.Maximum,
                Value = bitrateSlider.Value,
                Name = bitrateSlider.Name,
                Margin = ControlMarginInner,
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
