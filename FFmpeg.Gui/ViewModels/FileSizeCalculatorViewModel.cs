using FFmpeg.Gui.ServiceCode;
using MvvmCross.ViewModels;
using System;

namespace FFmpeg.Gui.ViewModels
{
    internal class FileSizeCalculatorViewModel : MvxViewModel
    {
        public FileSizeCalculatorViewModel()
        {
            AudioBitrate = 256;
            VideoBitrate = 2000;
            VideoLength = TimeSpan.FromMinutes(10);
        }

        private double _audioBitrate;
        private double _videoBitrate;
        private TimeSpan _videoLength;

        public double VideoBitrate
        {
            get { return _videoBitrate; }
            set
            {
                if (SetProperty(ref _videoBitrate, value))
                    DoCalculaton();
            }
        }

        public double AudioBitrate
        {
            get { return _audioBitrate; }
            set
            {
                if (SetProperty(ref _audioBitrate, value))
                    DoCalculaton();
            }
        }

        public TimeSpan VideoLength
        {
            get { return _videoLength; }
            set
            {
                if (SetProperty(ref _videoLength, value))
                    DoCalculaton();
            }
        }

        private void DoCalculaton()
        {
            FileSize = FileSizeCalcuator.CalculateFileSizes(VideoBitrate, AudioBitrate, VideoLength.TotalSeconds);
            RaisePropertyChanged(nameof(FileSize));
        }

        public long FileSize { get; private set; }
    }
}
