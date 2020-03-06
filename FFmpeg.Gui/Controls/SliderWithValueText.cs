﻿using System.Windows;
using System.Windows.Controls;

namespace FFmpeg.Gui.Controls
{
    internal class SliderWithValueText: Slider
    {
        public string ValueUnit
        {
            get { return (string)GetValue(ValueUnitProperty); }
            set { SetValue(ValueUnitProperty, value); }
        }

        public static readonly DependencyProperty ValueUnitProperty =
            DependencyProperty.Register("ValueUnit", typeof(string), typeof(SliderWithValueText), new PropertyMetadata(""));
    }
}
