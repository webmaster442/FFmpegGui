using System.Collections.Generic;
using System.Xml.Serialization;

namespace FFmpeg.Gui.Domain
{
    public class Preset
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [XmlAttribute]
        public string Extension { get; set; }

        [XmlArray]
        [XmlArrayItem(nameof(BitrateSlider), typeof(BitrateSlider))]
        public List<PresetControl> Controllers { get; set; }

        [XmlArray]
        public List<string> ArgumentCollection { get; set; }
    }
}
