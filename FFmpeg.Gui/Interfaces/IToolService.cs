using System.Collections.Generic;

namespace FFmpeg.Gui.Interfaces
{
    interface IToolService
    {
        IEnumerable<ITool> GetTools();
        void RunTool(ITool tool);
    }
}
