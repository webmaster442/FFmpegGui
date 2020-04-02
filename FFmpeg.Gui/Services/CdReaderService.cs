using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.ServiceCode;
using FFmpeg.Gui.ViewModels.ListItems;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FFmpeg.Gui.Services
{
    internal class CdReaderService : ICdReaderService
    {
        public Task<CdItemViewModel[]> GetTracks(string driveLetter)
        {
            return Task.Run<CdItemViewModel[]>(() =>
            {
                return GetTracksJob(driveLetter);
            });
        }

        private static CdItemViewModel[] GetTracksJob(string driveLetter)
        {
            using (var cd = new CdReader())
            {
                cd.Open(driveLetter[0]);
                cd.LoadCD();
                cd.LockCD();
                cd.Refresh();

                List<CdItemViewModel> results = new List<CdItemViewModel>(cd.NumberOfAudioTracks);

                for (int i = 1; i <= cd.NumberOfTracks; i++)
                {
                    Domain.Cd.CdTrackInfo trackInfo = cd.GetTrackInfo(i);
                    if (trackInfo.IsAudio)
                    {
                        results.Add(new CdItemViewModel
                        {
                            Name = $"Audio Track #{i}",
                            Length = TimeSpan.FromSeconds(trackInfo.Length),
                            Size = trackInfo.Size
                        });
                    }
                }
                return results.ToArray();
            }
        }
    }
}
