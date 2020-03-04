using System.Xml.Serialization;

namespace FFmpeg.Gui.Domain
{
    public class PresetControl
    {
        [XmlAttribute]
        public string Name { get; set; }
        public string Label { get; set; }
    }
}
