using System.Collections.Generic;

namespace FFmpeg.Gui.Domain
{
    internal class Session
    {
        public Preset CurrentPreset { get; set; }
        public List<string> InputFiles { get; set; }
    }
}
