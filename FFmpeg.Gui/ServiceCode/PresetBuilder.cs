//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Controls;
using System;
using System.Globalization;

namespace FFmpeg.Gui.ServiceCode
{
    internal static class PresetBuilder
    {
        internal static string ProcessSlider(string argument, string match, SliderWithValueText slider)
        {
            return argument.Replace(match, slider.Value.ToString(CultureInfo.InvariantCulture));
        }

        internal static string ProcessVideoScale(string argument, string match, VideoScaleInput videoScale)
        {
            if (videoScale.IsMaxSize)
            {
                return argument.Replace(match, string.Format("min({0},iw):min({1},ih)", videoScale.VideoWidth, videoScale.VideoHeight));
            }
            return argument.Replace(match, string.Format("{0}:{1}", videoScale.VideoWidth, videoScale.VideoHeight));
        }

        internal static string ProcessOptionSelector(string argument, string match, OptionSelector optionSelector)
        {
            return argument.Replace(match, optionSelector.SelectedOptionValue);
        }

        internal static string ProcessVideoTime(string argument, string match, string subname, TimeSpanInput videoTime)
        {
            if (subname == nameof(TimeSpanInputViewModel.EndTime))
            {
                if (videoTime.ViewModel.EndTime.TotalSeconds < 0)
                    return string.Empty;
                else
                {
                    TimeSpan duration = videoTime.ViewModel.EndTime - videoTime.ViewModel.StartTime;
                    return argument.Replace(match, duration.TotalSeconds.ToString(CultureInfo.InvariantCulture));
                }
            }
            if (subname == nameof(TimeSpanInputViewModel.StartTime))
            {
                return argument.Replace(match, videoTime.ViewModel.StartTime.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }
            return string.Empty;
        }
    }
}
