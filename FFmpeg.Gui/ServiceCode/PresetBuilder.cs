using FFmpeg.Gui.Controls;
using System;
using System.Globalization;

namespace FFmpeg.Gui.ServiceCode
{
    internal static class PresetBuilder
    {
        internal static string RenderSlider(string argument, string match, SliderWithValueText slider)
        {
            return argument.Replace(match, slider.Value.ToString(CultureInfo.InvariantCulture));
        }

        internal static string RenderVideoScale(string argument, string match, VideoScaleInput videoScale)
        {
            return argument.Replace(match, string.Format("{0}:{1}", videoScale.VideoWidth, videoScale.VideoHeight));
        }

        internal static string RenderVideoTime(string argument, string match, string subname, TimeSpanInput videoTime)
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
