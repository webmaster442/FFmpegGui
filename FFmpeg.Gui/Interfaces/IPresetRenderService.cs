using FFmpeg.Gui.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetRenderService
    {
        void RenderPreset(StackPanel target, Preset preset);
    }
}
