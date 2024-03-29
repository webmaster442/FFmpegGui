﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Infrastructure;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.ViewModels;
using FFmpeg.Gui.Views;
using MahApps.Metro.Controls;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FFmpeg.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IMvxWindow, IMvxWpfView, IDisposable
    {
        private IMvxViewModel? _viewModel;
        private IMvxBindingContext? _bindingContext;
        private bool _unloaded = false;

        public MainWindow()
        {
            Identifier = string.Empty;
            Closed += MvxWindow_Closed;
            Unloaded += MvxWindow_Unloaded;
            Loaded += MvxWindow_Loaded;
            Initialized += MvxWindow_Initialized;
            InitializeComponent();
            DataContext = Mvx.IoCProvider.Resolve<MainViewModel>();
            SwitchTabIndex(0);

            if (FindResource("icon-ffmpeg") is Viewbox icon)
            {
                icon.Width = 32;
                icon.Height = 32;
                Icon = icon.ToBitmapSource();
            }
        }

        private void MvxWindow_Initialized(object? sender, EventArgs e)
        {
            if (this == Application.Current.MainWindow
                && Application.Current is MvvmCross.Platforms.Wpf.Views.MvxApplication mvxApp)
            {
                mvxApp.ApplicationInitialized();
            }
        }

        public IMvxViewModel? ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = value;
                if (BindingContext != null)
                    BindingContext.DataContext = value;
            }
        }

        public IMvxBindingContext? BindingContext
        {
            get
            {
                if (_bindingContext != null)
                    return _bindingContext;

                if (Mvx.IoCProvider != null)
                    this.CreateBindingContext();

                return _bindingContext;
            }
            set => _bindingContext = value;
        }

        public string Identifier { get; set; }

        private void MvxWindow_Closed(object? sender, EventArgs e) => Unload();

        private void MvxWindow_Unloaded(object sender, RoutedEventArgs e) => Unload();

        private void MvxWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MainWindow()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Unloaded -= MvxWindow_Unloaded;
                Loaded -= MvxWindow_Loaded;
                Closed -= MvxWindow_Closed;
            }
        }

        private void Unload()
        {
            if (!_unloaded)
            {
                ViewModel?.ViewDisappearing();
                ViewModel?.ViewDisappeared();
                ViewModel?.ViewDestroy();
                _unloaded = true;
                Dispose();
            }
        }

        private void SwitchTabIndex(int increment)
        {
            if (increment < 0
                && MainTabs.SelectedIndex + increment >= 0)
            {
                MainTabs.SelectedIndex += increment;
            }
            else if (increment > 0
                     && MainTabs.SelectedIndex + increment <= MainTabs.Items.Count - 1)
            {
                MainTabs.SelectedIndex += increment;
            }
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            NextButton.IsEnabled = MainTabs.SelectedIndex < MainTabs.Items.Count - 1;
            PreviousButton.IsEnabled = MainTabs.SelectedIndex > 0;
        }

        private void MainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtons();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => SwitchTabIndex(-1));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => SwitchTabIndex(+1));
        }

        private void MvxWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is MainViewModel mvm)
            {
                mvm.PresetSelectorVM.RenderTarget = PresetSelector;
                mvm.JobVM.RenderTarget = PresetSelector;
            }
        }

        internal void ShowChangeLog()
        {
            if (DataContext is MainViewModel mvm)
            {
                mvm.ToolFlyoutOpen = false;
                ToolPopup.Content = new ChangeLogView();
                ToolPopup.Title = "Changelog";
                ToolPopup.Visibility = Visibility.Visible;
            }
        }

        internal void ShowToolPopup(ITool tool)
        {
            if (DataContext is MainViewModel mvm)
            {
                mvm.ToolFlyoutOpen = false;
                tool.ConstructAndAssociateViewModel();
                ToolPopup.Content = tool;
                ToolPopup.Title = tool.Title;
                ToolPopup.Visibility = Visibility.Visible;
            }
        }
    }
}
