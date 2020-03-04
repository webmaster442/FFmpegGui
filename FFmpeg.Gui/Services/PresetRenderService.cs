using FFmpeg.Gui.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace FFmpeg.Gui.Services
{
    /// <summary>
    /// Renders a preset to UI Controls
    /// </summary>
    internal class PresetRenderService
    {
        public void RenderPreset(StackPanel target, Preset preset)
        {
            foreach (var controller in preset.Controllers)
            {
                Control rendered = Render(controller);
                target.Children.Add(rendered);
            }
        }

        private Control Render(PresetControl controller)
        {
            throw new NotImplementedException();
        }
    }
}
