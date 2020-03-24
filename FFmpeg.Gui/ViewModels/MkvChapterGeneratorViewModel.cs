//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;

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
            throw new NotImplementedException();
        }

        private void OnLoadText()
        {
            throw new NotImplementedException();
        }
    }
}
