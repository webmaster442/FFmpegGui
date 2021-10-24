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
    /// Interaction logic for FileSizeCalculator.xaml
    /// </summary>
    public partial class FileSizeCalculator : MvxWpfView, ITool
    {
        public FileSizeCalculator()
        {
            InitializeComponent();
        }

        public string Title => "File size calculator";

        public string Icon => "icon-calculator";

        public void ConstructAndAssociateViewModel()
        {
            DataContext = Mvx.IoCProvider.Resolve<FileSizeCalculatorViewModel>();
        }
    }
}
