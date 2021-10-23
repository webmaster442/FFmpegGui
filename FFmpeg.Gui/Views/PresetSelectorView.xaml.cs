//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Controls;
using FFmpeg.Gui.Interfaces;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;

namespace FFmpeg.Gui.Views
{
    /// <summary>
    /// Interaction logic for PresetSelectorView.xaml
    /// </summary>
    public partial class PresetSelectorView : MvxWpfView, IRenderPanel
    {
        public PresetSelectorView()
        {
            InitializeComponent();
        }

        public bool IsValid
        {
            get
            {
                foreach (var item in RenderPanel.Children)
                {
                    if (item is IValidatableControl validatable
                        && !validatable.IsValid)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public void ClearItems()
        {
            RenderPanel.Children.Clear();
        }

        public FrameworkElement? GetElement(string name)
        {
            foreach (var element in RenderPanel.Children)
            {
                if (element is FrameworkElement frameworkelement
                    && frameworkelement.Name == name)
                {
                    return frameworkelement;
                }
            }
            return null;
        }

        public void Render(FrameworkElement element)
        {
            RenderPanel.Children.Add(element);
        }
    }
}
