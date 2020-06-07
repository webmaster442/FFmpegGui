using FFmpeg.Gui.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFmpeg.Gui.Services
{
    internal class ToolService : IToolService
    {
        public IEnumerable<ITool> GetTools()
        {
            var codeAssembly = typeof(ITool).Assembly;

            var tools = codeAssembly.GetTypes()
                .Where(t => typeof(ITool).IsAssignableFrom(t) && t.IsPublic && !t.IsInterface && !t.IsAbstract);

            foreach (var tool in tools)
            {
                if (Activator.CreateInstance(tool) is ITool toolInstance)
                {
                    yield return toolInstance;
                }
            }
        }

        public void RunTool(ITool tool)
        {
            var mainWin = App.Current.MainWindow as MainWindow;
            mainWin?.ShowToolPopup(tool);
        }
    }
}
