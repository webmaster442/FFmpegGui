using FFmpeg.Gui.Presets.Controls;
using System.Collections.Generic;

namespace FFmpeg.Gui.Presets
{
    public class Preset
    {
        public string Name { get; set; }

        public string TargetExtension { get; set; }

        public string Description { get; set; }

        public List<ControlBase> Controllers { get; set; }

        public List<string> ArgumentCollection { get; set; }

        public Preset()
        {
            Name = string.Empty;
            TargetExtension = string.Empty;
            Description = string.Empty;
            Controllers = new List<ControlBase>();
            ArgumentCollection = new List<string>();
        }
    }
}
