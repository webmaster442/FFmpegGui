using System.Collections.Generic;
using System.Xml.Serialization;

namespace FFmpeg.Gui.Domain
{
    public class Preset
    {
        [XmlAttribute]
        public string Extension { get; set; }

        [XmlArray]
        [XmlArrayItem(nameof(BitrateSlider), typeof(BitrateSlider))]
        public List<PresetControl> Controllers { get; set; }

        [XmlArray]
        public List<string> Arguments { get; set; }
    }
}
