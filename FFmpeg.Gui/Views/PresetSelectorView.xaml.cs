using FFmpeg.Gui.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace FFmpeg.Gui.Views
{
    /// <summary>
    /// Interaction logic for PresetSelectorView.xaml
    /// </summary>
    public partial class PresetSelectorView : UserControl, IRenderPanel
    {
        public PresetSelectorView()
        {
            InitializeComponent();
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
