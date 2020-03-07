using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Services;
using FFmpeg.Gui.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace FFmpeg.Gui
{
    internal sealed class Builder: MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDialogService, DialogService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IPresetReaderService, PresetReaderService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IPresetRenderService, PresetRenderService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IPresetBuilderService, PresetBuilderService>();
            Mvx.IoCProvider.RegisterType<MainViewModel>();
        }
    }
}
