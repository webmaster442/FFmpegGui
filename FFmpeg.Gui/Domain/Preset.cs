﻿using System.Collections.Generic;

namespace FFmpeg.Gui.Domain
{
    public class Preset
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Extension { get; set; }

        public List<PresetControl> Controllers { get; set; }

        public List<string> ArgumentCollection { get; set; }
    }
}
