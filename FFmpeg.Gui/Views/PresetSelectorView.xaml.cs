﻿using FFmpeg.Gui.Interfaces;
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

        public void ClearItems()
        {
            RenderPanel.Children.Clear();
        }

        public FrameworkElement GetElement(string name)
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
