//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Xml.Serialization;

namespace FFmpeg.Gui.Domain.Mkv
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ChaptersChapterAtom
    {
        public string ChapterTimeStart { get; set; }

        public ChaptersChapterAtomChapterDisplay ChapterDisplay { get; set; }

        public ChaptersChapterAtom()
        {
            ChapterTimeStart = string.Empty;
            ChapterDisplay = new ChaptersChapterAtomChapterDisplay();
        }
    }

}
