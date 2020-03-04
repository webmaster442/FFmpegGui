using System.Collections.Generic;
using System.Xml.Serialization;

namespace FFmpeg.Gui.Domain
{
    public class BitrateSlider: PresetControl
    {
        [XmlAttribute]
        public int Minimum { get; set; }
        [XmlAttribute]
        public int Maximum { get; set; }
        public List<int> PresetValues { get; set; }
    }
}
