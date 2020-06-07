//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Xml.Serialization;

namespace FFmpeg.Gui.Domain.Mkv
{
    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class ChaptersChapterAtomChapterDisplay
    {
        public string ChapterString { get; set; }

        public string ChapterLanguage { get; set; }

        public ChaptersChapterAtomChapterDisplay()
        {
            ChapterString = string.Empty;
            ChapterLanguage = string.Empty;
        }
    }
}
