using FFmpeg.Gui.ViewModels;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;


namespace FFmpeg.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        public App()
        {
            this.RegisterSetupType<MvxWpfSetup<Builder>>();
        }

        public override void ApplicationInitialized()
        {
            MainWindow.DataContext = new MainViewModel();
        }
    }
}
