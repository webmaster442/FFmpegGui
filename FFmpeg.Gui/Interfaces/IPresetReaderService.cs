using FFmpeg.Gui.Domain;

namespace FFmpeg.Gui.Interfaces
{
    internal interface IPresetReaderService
    {
        Preset ReadPresetXml(string xmlFile);
    }
}
