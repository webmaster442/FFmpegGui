//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Infrastructure;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Properties;
using FFmpeg.Gui.ViewModels.ListItems;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace FFmpeg.Gui.ViewModels
{
    internal class CdReaderViewModel: MvxViewModel
    {
        private readonly ICdReaderService _cdReaderService;
        private readonly IDialogService _dialogService;
        private string _targetDirectory;

        private bool _reading;
        private string _driveLetter;

        private readonly CancellationTokenSource _cts;

        private readonly Progress<long> _progressReporter;
        private long _done;
        private long _total;

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

        public long Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }

        public long Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }

        public ObservableCollectionExt<string> CdRomDrives { get; }

        public BindingList<CdItemViewModel> CdItems { get; }

        public MvxCommand SelectOutDirCommand { get; }

        public MvxCommand OpenDriveCommand { get; }

        public MvxCommand ReadCommand { get; }

        public MvxCommand CancelCommand { get; }

        public MvxCommand SelectAllCommand { get; }

        public MvxCommand DeSelectAllCommand { get; }


        public CdReaderViewModel(ICdReaderService cdReaderService,
                                 IDialogService dialogService)
        {
            _cdReaderService = cdReaderService;
            _dialogService = dialogService;

            var drives = DriveInfo.GetDrives()
                .Where(drive => drive.DriveType == DriveType.CDRom)
                .Select(drive => drive.Name);

            CdRomDrives = new ObservableCollectionExt<string>(drives);

            if (CdRomDrives.Count > 0)
                DriveLetter = CdRomDrives[0];

            CdItems = new BindingList<CdItemViewModel>();
            CdItems.ListChanged += OnCdItemsChanged;

            OpenDriveCommand = new MvxCommand(OnOpenDrive, CanDoAction);
            SelectOutDirCommand = new MvxCommand(OnSelectOutDir, CanDoAction);
            ReadCommand = new MvxCommand(OnRead, CanRead);
            CancelCommand = new MvxCommand(OnCancel);
            SelectAllCommand = new MvxCommand(() => SetSelectionState(true), CanSelect);
            DeSelectAllCommand = new MvxCommand(() => SetSelectionState(false), CanSelect);

            _cts = new CancellationTokenSource();
            _progressReporter = new Progress<long>();
            _progressReporter.ProgressChanged += OnReportProgress;
        }

        private void OnReportProgress(object sender, long e)
        {
            Done += e;
        }

        private void OnCdItemsChanged(object sender, ListChangedEventArgs e)
        {
            SelectAllCommand.RaiseCanExecuteChanged();
            DeSelectAllCommand.RaiseCanExecuteChanged();
            ReadCommand.RaiseCanExecuteChanged();
        }

        private void OnSelectOutDir()
        {
            if (_dialogService.ShowFolderSelect(out string path))
            {
                TargetDirectory = path;
            }
            ReadCommand.RaiseCanExecuteChanged();
        }

        private bool CanDoAction()
        {
            return 
                !Reading 
                && !string.IsNullOrEmpty(DriveLetter)
                && CdRomDrives.Count > 0;
        }


        private async void OnOpenDrive()
        {
            try
            {
                Reading = true;
                CdItems.Clear();
                CdItemViewModel[] cdTracks = await _cdReaderService.GetTracks(DriveLetter);
                CdItems.AddRange(cdTracks);
                await RaisePropertyChanged(nameof(CdItems));
                Reading = false;
            }
            catch (Exception)
            {
                _dialogService.ShowError(Resources.Error_CdRead);
                Reading = false;
                CdItems.Clear();
            }
            finally
            {
                SelectAllCommand.RaiseCanExecuteChanged();
                DeSelectAllCommand.RaiseCanExecuteChanged();
                ReadCommand.RaiseCanExecuteChanged();
            }
        }

        private void SetSelectionState(bool state)
        {
            foreach (var item in CdItems)
                item.IsSelected = state;
        }

        private bool CanSelect()
        {
            return CdItems.Count > 0;
        }

        private void OnCancel()
        {
            _cts.Cancel();
        }

        private bool CanRead()
        {
            return
                CanDoAction()
                && Directory.Exists(TargetDirectory)
                && CdItems.Any(item => item.IsSelected == true);
        }

        private async void OnRead()
        {
            try
            {
                Done = 0;
                Reading = true;
                var selectedTracks = CdItems.Where(i => i.IsSelected);
                Total = selectedTracks.Sum(i => i.Size);
                var reuslt = await _cdReaderService.ReadTracks(DriveLetter, selectedTracks, TargetDirectory, _progressReporter, _cts.Token);
                Reading = false;
            }
            catch (Exception)
            {
                _dialogService.ShowError(Resources.Error_CdRead);
                Reading = false;
                Done = 0;
            }
        }

    }
}
