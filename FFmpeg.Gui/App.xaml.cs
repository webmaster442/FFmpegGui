//-----------------------------------------------------------------------------
// (c) 2020-201 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace FFmpeg.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<Setup>();
        }
    }
}
