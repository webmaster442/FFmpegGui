//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace FFmpeg.Gui.ViewModels
{
    internal class ToolsViewModel
    {
        private readonly IToolService _toolService;

        public ToolsViewModel(IToolService toolService)
        {
            _toolService = toolService;
            Tools = new ObservableCollection<string>(_toolService.GetTools().Select(t => t.Title));
        }

        public ObservableCollection<string> Tools { get; }

    }
}
