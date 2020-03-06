using FFmpeg.Gui.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Views;

namespace FFmpeg.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MvxWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Mvx.IoCProvider.Resolve<MainViewModel>();
        }
    }
}
