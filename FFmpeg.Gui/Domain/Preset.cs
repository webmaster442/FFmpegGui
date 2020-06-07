//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace FFmpeg.Gui.Domain
{
    public class Preset
    {
        public string Name { get; set; }

        public string TargetExtension { get; set; }

        public string Description { get; set; }

        public List<PresetControl> Controllers { get; set; }

        public List<string> ArgumentCollection { get; set; }

        public Preset()
        {
            Name = string.Empty;
            TargetExtension = string.Empty;
            Description = string.Empty;
            Controllers = new List<PresetControl>();
            ArgumentCollection = new List<string>();
        }
    }
}
