//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain.Mkv;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using MkvChapterGenerator;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace FFmpeg.Gui.ViewModels
{
    internal class MkvChapterGeneratorViewModel: MvxViewModel
    {
        private readonly IDialogService _dialogService;
        private string _inputText;

        public MvxCommand SaveXmlCommand { get; }
        public MvxCommand LoadTextCommand { get; }

        public string InputText
        {
            get { return _inputText; }
            set { SetProperty(ref _inputText, value); }
        }

        public MkvChapterGeneratorViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            LoadTextCommand = new MvxCommand(OnLoadText);
            SaveXmlCommand = new MvxCommand(OnSaveXml);
        }

        private void OnSaveXml()
        {
            if (_dialogService.ShowSaveFileDialog("XML files|*.xml", out string file))
            {
                try
                {
                    var lines = InputText.Split('\n').Select(l => l.Trim());
                    Chapters xml = MkvXmlFactory.BuildChapters(lines);
                    SerializeXML(xml, file);
                }
                catch (IOException)
                {
                    _dialogService.ShowError(Resources.Error_FileWrite);
                }
            }
        }

        private static void SerializeXML(Chapters xml, string target)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Chapters));
            var Namespaces = new XmlSerializerNamespaces();
            //no namespaces
            Namespaces.Add("", "");
            using var outp = File.Create(target);
            xs.Serialize(outp, xml, Namespaces);
        }

        private void OnLoadText()
        {
            if (_dialogService.ShowFileSelector(false, "Text files|*.txt", out string[] files))
            {
                try
                {
                    File.ReadAllText(files[0]);
                }
                catch (IOException)
                {
                    _dialogService.ShowError(Resources.Error_FileLoad);
                }
            }
        }
    }
}
