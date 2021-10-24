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
    /// Interaction logic for MkvChapterGeneratorView.xaml
    /// </summary>
    public partial class MkvChapterGeneratorView : MvxWpfView, ITool
    {
        public MkvChapterGeneratorView()
        {
            InitializeComponent();
        }

        public string Title => "MkvToolnix Chapter Generator";

        public string Icon => "icon-mkv";

        public void ConstructAndAssociateViewModel()
        {
            DataContext = Mvx.IoCProvider.Resolve<MkvChapterGeneratorViewModel>();
        }
    }
}
