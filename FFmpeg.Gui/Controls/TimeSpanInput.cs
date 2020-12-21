//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Windows.Controls;

namespace FFmpeg.Gui.Controls
{
    internal class TimeSpanInput : Control, IValidatableControl
    {
        public TimeSpanInputViewModel ViewModel { get; }

        public bool IsValid => !ViewModel.HasErrors;

        public TimeSpanInput()
        {
            ViewModel = new TimeSpanInputViewModel();
            DataContext = ViewModel;
        }
    }
}
