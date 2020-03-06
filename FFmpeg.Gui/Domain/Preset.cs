using System.Collections.Generic;
using System.Xml.Serialization;

namespace FFmpeg.Gui.Domain
{
    public class Preset
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Extension { get; set; }

        public PresetControl[] Controllers { get; set; }

        public string[] ArgumentCollection { get; set; }
    }
}
