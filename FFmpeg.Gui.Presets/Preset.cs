using FFmpeg.Gui.Presets.Controls;
using System;

namespace FFmpeg.Gui.Presets
{
    public record Preset
    {
        public string Name { get; set; }

        public string TargetExtension { get; set; }

        public string Description { get; set; }

        public ControlBase[] Controllers { get; set; }

        public string[] ArgumentCollection { get; set; }

        public string Category { get; set; }

        public Preset()
        {
            Name = string.Empty;
            Category = "Unknown";
            TargetExtension = string.Empty;
            Description = string.Empty;
            Controllers = Array.Empty<ControlBase>();
            ArgumentCollection = Array.Empty<string>();
        }
    }
}
