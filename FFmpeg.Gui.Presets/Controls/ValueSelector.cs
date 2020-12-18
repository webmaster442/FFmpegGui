//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace FFmpeg.Gui.Presets.Controls
{
    public class ValueSelector : ControlBase
    {
        public Dictionary<string, string> Options { get; set; }
        public string SelectedOptionKey { get; set; }

        public ValueSelector()
        {
            Options = new Dictionary<string, string>();
            SelectedOptionKey = string.Empty;
        }
    }
}
