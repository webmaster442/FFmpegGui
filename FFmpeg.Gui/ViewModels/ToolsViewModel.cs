//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.ViewModels.ListItems;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FFmpeg.Gui.ViewModels
{
    internal class ToolsViewModel
    {
        private readonly IToolService _toolService;
        private readonly List<ITool> _tools;

        public MvxCommand<string> LaunchToolCommand { get; }

        public ToolsViewModel(IToolService toolService)
        {
            _toolService = toolService;

            _tools = _toolService.GetTools().ToList();

            Tools = new ObservableCollection<ToolItemModel>(_tools.Select(t => new ToolItemModel
            {
                Name = t.Title,
                Icon = string.IsNullOrEmpty(t.Icon) ? null : App.Current.FindResource(t.Icon),
            }));
            LaunchToolCommand = new MvxCommand<string>(OnLaunchTool);
        }

        public ObservableCollection<ToolItemModel> Tools { get; }

        private void OnLaunchTool(string obj)
        {
            var tool = _tools.FirstOrDefault(t => t.Title == obj);
            if (tool != null)
            {
                _toolService.RunTool(tool);
            }
        }

    }
}
