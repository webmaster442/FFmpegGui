//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.ViewModels.ListItems;
using System.Threading.Tasks;

namespace FFmpeg.Gui.Interfaces
{
    internal interface ICdReaderService
    {
        Task<CdItemViewModel[]> GetTracks(string driveLetter);
    }
}
