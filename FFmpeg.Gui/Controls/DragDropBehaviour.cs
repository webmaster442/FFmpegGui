//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FFmpeg.Gui.Controls
{
    internal static class DragDropBehaviour
    {
        public static ICommand GetFileDraggedInCommmand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(FileDraggedInCommmandProperty);
        }

        public static void SetFileDraggedInCommmand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(FileDraggedInCommmandProperty, value);
        }


        public static readonly DependencyProperty FileDraggedInCommmandProperty =
            DependencyProperty.RegisterAttached("FileDraggedInCommmand", typeof(ICommand), typeof(DragDropBehaviour), new PropertyMetadata(null, CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox lbox)
            {
                lbox.AllowDrop = true;
                lbox.DragEnter += Lbox_DragEnter;
                lbox.Drop += Lbox_Drop;
            }
        }

        private static void Lbox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private static void Lbox_Drop(object sender, DragEventArgs e)
        {
            if (sender is DependencyObject dependencyObject)
            {
                ICommand cmd = GetFileDraggedInCommmand(dependencyObject);
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (cmd?.CanExecute(files) ?? false)
                    cmd?.Execute(files);
            }
        }
    }
}
