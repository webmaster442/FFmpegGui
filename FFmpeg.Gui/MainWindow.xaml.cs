using FFmpeg.Gui.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using System.Windows.Controls;

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
            SwitchTabIndex(0);
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
    }
}
