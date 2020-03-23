using FFmpeg.Gui.Properties;
using MvvmCross.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace FFmpeg.Gui.Controls
{
    internal class TimeSpanInputViewModel : MvxViewModel, INotifyDataErrorInfo
    {
        private bool _endTimeIsSeconds;
        private bool _startTimeIsSeconds;
        private string _startTimeDisplayText;
        private string _endTimeDisplayText;
        private TimeSpan _startTime;
        private TimeSpan _endTime;

        private readonly Dictionary<string, string> _errors;

        public TimeSpanInputViewModel()
        {
            _errors = new Dictionary<string, string>();
        }

        public bool StartTimeIsSeconds
        {
            get { return _startTimeIsSeconds; }
            set
            {
                if (SetProperty(ref _startTimeIsSeconds, value))
                {
                    TimeSpanToString(StartTime, value, ref _startTimeDisplayText, nameof(StartTimeDisplayText));
                }
            }
        }

        public bool EndTimeIsSeconds
        {
            get { return _endTimeIsSeconds; }
            set
            {
                if (SetProperty(ref _endTimeIsSeconds, value))
                {
                    TimeSpanToString(EndTime, value, ref _endTimeDisplayText, nameof(EndTimeDisplayText));
                }
            }
        }

        public string StartTimeDisplayText
        {
            get { return _startTimeDisplayText; }
            set
            {
                if (SetProperty(ref _startTimeDisplayText, value))
                {
                    StringToTimeSpan(value, StartTimeIsSeconds, ref _startTime, nameof(StartTime), nameof(StartTimeDisplayText));
                }
            }
        }

        public string EndTimeDisplayText
        {
            get { return _endTimeDisplayText; }
            set
            {
                if (SetProperty(ref _endTimeDisplayText, value))
                {
                    StringToTimeSpan(value, EndTimeIsSeconds, ref _endTime, nameof(EndTime), nameof(EndTimeDisplayText));
                }
            }
        }

        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                SetProperty(ref _startTime, value);
                TimeSpanToString(value, StartTimeIsSeconds, ref _startTimeDisplayText, nameof(StartTimeDisplayText));
            }
        }

        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                SetProperty(ref _endTime, value);
                TimeSpanToString(value, EndTimeIsSeconds, ref _endTimeDisplayText, nameof(EndTimeDisplayText));
            }
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool HasErrors => _errors.Any(kv => !string.IsNullOrEmpty(kv.Value));

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            string error = string.Empty;
            _errors.TryGetValue(propertyName, out error);
            yield return error;
        }

        private void StringToTimeSpan(string input, bool isSeconds, ref TimeSpan field, string PropertyChanged, string validateProperty)
        {
            if (isSeconds)
            {
                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsedDouble))
                {
                    _errors.Remove(validateProperty);
                    field = TimeSpan.FromSeconds(parsedDouble);
                    RaisePropertyChanged(PropertyChanged);
                }
                else
                {
                    _errors.TryAdd(validateProperty, Resources.Error_Timespan_IncorrectFormat);
                }
            }
            else
            {
                if (TimeSpan.TryParse(input, out TimeSpan parsed))
                {
                    _errors.Remove(validateProperty);
                    field = parsed;
                    RaisePropertyChanged(PropertyChanged);
                }
                else
                {
                    _errors.TryAdd(validateProperty, Resources.Error_Timespan_IncorrectFormat);
                }
            }
            RaiseErrorsChanged(validateProperty);
        }

        private void TimeSpanToString(TimeSpan value, bool isSeconds, ref string field, string PropertyChanged)
        {
            if (isSeconds)
            {
                field = value.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                field = value.ToString();
            }
            _errors.Remove(PropertyChanged);
            RaisePropertyChanged(PropertyChanged);
            RaiseErrorsChanged(PropertyChanged);
        }
    }
}
