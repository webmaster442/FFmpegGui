//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain.Mkv;
using System.Collections.Generic;

namespace MkvChapterGenerator
{
    internal static class MkvXmlFactory
    {
        public static Chapters BuildChapters(IEnumerable<string> lines)
        {
            var result = new Chapters
            {
                EditionEntry = CreateChapters(lines)
            };
            return result;
        }

        private static ChaptersChapterAtom[] CreateChapters(IEnumerable<string> lines)
        {
            var results = new List<ChaptersChapterAtom>();
            foreach(var line in lines)
            {
                string[] parts = line.Split(' ');
                string timestr = parts[0];
                string title = line.Replace($"{timestr} ", "");

                results.Add(new ChaptersChapterAtom
                {
                    ChapterTimeStart = FormatTimeString(timestr),
                    ChapterDisplay = new ChaptersChapterAtomChapterDisplay
                    {
                        ChapterLanguage = "eng",
                        ChapterString = title
                    }
                });
            }
            return results.ToArray();
        }

        private static string FormatTimeString(string timestr)
        {
            var parts = timestr.Split(':');
            return parts.Length switch
            {
                1 => $"00:00:{parts[0]}.000",
                2 => $"00:{parts[0]}:{parts[1]}.000",
                3 => $"{parts[0]}:{parts[1]}:{parts[2]}.000",
                _ => "",
            };
        }

    }
}
