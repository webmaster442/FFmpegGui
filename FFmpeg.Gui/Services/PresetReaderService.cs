using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using System;
using System.IO;
using System.Xml.Serialization;

namespace FFmpeg.Gui.Services
{
    internal class PresetReaderService
    {
        private readonly XmlSerializer _serializer;
        private readonly IDialogService _dialogService;

        public PresetReaderService(IDialogService dialogService)
        {
            _serializer = new XmlSerializer(typeof(Preset));
            _dialogService = dialogService;
        }

        public Preset ReadPresetXml(string xmlFile)
        {
            try
            {
                using (var file = File.OpenRead(xmlFile))
                {
                    return (Preset)_serializer.Deserialize(file);
                }
            }
            catch (IOException)
            {
                _dialogService.ShowError(Resources.Error_PresetXMLSerialize);
                return null;
            }
            catch (InvalidOperationException)
            {
                _dialogService.ShowError(Resources.Error_PresetXMLSerialize);
                return null;
            }
        }
    }
}
