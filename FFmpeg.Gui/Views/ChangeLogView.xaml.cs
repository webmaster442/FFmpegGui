﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using MvvmCross.Platforms.Wpf.Views;

namespace FFmpeg.Gui.Views
{
    /// <summary>
    /// Interaction logic for ChangeLogView.xaml
    /// </summary>
    public partial class ChangeLogView : MvxWpfView
    {
        public ChangeLogView()
        {
            InitializeComponent();
            TextDisplay.Text = Properties.Resources.Changelog;
        }
    }
}
