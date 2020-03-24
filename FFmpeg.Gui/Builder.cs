//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

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
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IErrorDisplayService, ErrorDisplayService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IToolService, ToolService>();
            Mvx.IoCProvider.RegisterType<MainViewModel>();
            Mvx.IoCProvider.RegisterType<MkvChapterGeneratorViewModel>();
        }
    }
}
