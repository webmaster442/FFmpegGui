//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Infrastructure;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.ViewModels.ListItems;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Linq;

namespace FFmpeg.Gui.ViewModels
{
    internal class CdReaderViewModel: MvxViewModel
    {
        private readonly ICdReaderService _cdReaderService;
        private readonly IDialogService _dialogService;
        private string _targetDirectory;

        private bool driveOpen = false;
        private bool _reading;
        private string _driveLetter;

        public bool Reading
        {
            get { return _reading; }
            set 
            { 
                SetProperty(ref _reading, value);
                OpenDriveCommand.RaiseCanExecuteChanged();
                SelectOutDirCommand.RaiseCanExecuteChanged();
                ReadCommand.RaiseCanExecuteChanged();
            }
        }

        public string DriveLetter
        {
            get { return _driveLetter; }
            set { SetProperty(ref _driveLetter, value); }
        }

        public string TargetDirectory
        {
            get { return _targetDirectory; }
            set { SetProperty(ref _targetDirectory, value); }
        }

        public ObservableCollectionExt<string> CdRomDrives { get; }

        public ObservableCollectionExt<CdItemViewModel> CdItems { get; }

        public MvxCommand SelectOutDirCommand { get; }

        public MvxCommand OpenDriveCommand { get; }

        public MvxCommand ReadCommand { get; }

        public MvxCommand CancelCommand { get; }


        public CdReaderViewModel(ICdReaderService cdReaderService,
                                 IDialogService dialogService)
        {
            _cdReaderService = cdReaderService;
            _dialogService = dialogService;

            var drives = DriveInfo.GetDrives()
                .Where(drive => drive.DriveType == DriveType.CDRom)
                .Select(drive => drive.Name);

            CdRomDrives = new ObservableCollectionExt<string>(drives);
            CdItems = new ObservableCollectionExt<CdItemViewModel>();

            OpenDriveCommand = new MvxCommand(OnOpenDrive, CanDoAction);
            SelectOutDirCommand = new MvxCommand(OnSelectOutDir, CanDoAction);
            ReadCommand = new MvxCommand(OnRead, CanDoAction);
            CancelCommand = new MvxCommand(OnCancel);
        }

        private void OnSelectOutDir()
        {
            if (_dialogService.ShowFolderSelect(out string path))
            {
                TargetDirectory = path;
            }
        }

        private bool CanDoAction()
        {
            return !Reading;
        }


        private async void OnOpenDrive()
        {
            Reading = true;
            CdItems.Clear();
            CdItemViewModel[] cdTracks = await _cdReaderService.GetTracks(DriveLetter);
            CdItems.AddRange(cdTracks);
            Reading = false;
        }

        private void OnCancel()
        {
            throw new NotImplementedException();
        }

        private bool CanRead()
        {
            throw new NotImplementedException();
        }

        private void OnRead()
        {
            throw new NotImplementedException();
        }

    }
}
