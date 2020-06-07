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
    public class Chapters
    {
        [XmlArrayItem("ChapterAtom")]
        public ChaptersChapterAtom[] EditionEntry { get; set; }

        public Chapters()
        {
            EditionEntry = new ChaptersChapterAtom[0];
        }
    }
}
