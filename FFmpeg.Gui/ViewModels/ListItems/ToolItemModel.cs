//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace FFmpeg.Gui.ViewModels.ListItems
{
    internal class ToolItemModel
    {
        public string Name { get; set; }
        public object? Icon { get; set; }

        public ToolItemModel()
        {
            Name = string.Empty;
        }
    }
}
