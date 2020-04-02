//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Views;

namespace FFmpeg.Gui.Views
{
    /// <summary>
    /// Interaction logic for CdReaderView.xaml
    /// </summary>
    public partial class CdReaderView : MvxWpfView, ITool
    {
        public CdReaderView()
        {
            InitializeComponent();
        }

        public string Title => "Audio Cd Reader";

        public void ConstructAndAssociateViewModel()
        {
            DataContext = Mvx.IoCProvider.Resolve<CdReaderViewModel>();
        }
    }
}
