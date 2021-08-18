using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Wpf.Core;

namespace FFmpeg.Gui
{
    internal class Setup : MvxWpfSetup<Builder>
    {
        protected override ILoggerFactory? CreateLogFactory()
        {
            //No logging for now
            return null;
        }

        protected override ILoggerProvider? CreateLogProvider()
        {
            //No logging for now
            return null;
        }
    }
}
