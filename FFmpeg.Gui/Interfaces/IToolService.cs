using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpeg.Gui.Interfaces
{
    interface IToolService
    {
        IEnumerable<ITool> GetTools();
        void RunTool(ITool tool);
    }
}
