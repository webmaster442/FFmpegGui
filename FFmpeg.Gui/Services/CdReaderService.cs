using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.ServiceCode;
using FFmpeg.Gui.ViewModels.ListItems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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

                bool prepare = cd.LoadCD() && cd.LockCD() && cd.Refresh();

                if (!prepare)
                    return new CdItemViewModel[0];

                List<CdItemViewModel> results = new List<CdItemViewModel>(cd.NumberOfAudioTracks);

                for (int i = 1; i <= cd.NumberOfTracks; i++)
                {
                    Domain.Cd.CdTrackInfo? trackInfo = cd.GetTrackInfo(i);
                    if (trackInfo != null && trackInfo.IsAudio)
                    {
                        results.Add(new CdItemViewModel($"Audio Track #{i}")
                        {
                            Length = TimeSpan.FromSeconds(trackInfo.Length),
                            Size = trackInfo.Size,
                            IsSelected = true,
                            Track = i
                        });
                    }
                }

                cd.UnLockCD();

                return results.ToArray();
            }
        }

        public Task<bool> ReadTracks(string driveLetter, IEnumerable<CdItemViewModel> tracks, string outDir, IProgress<long> progress, CancellationToken token)
        {
            return Task.Run<bool>(() =>
            {
                return ReadTracksJob(driveLetter, tracks, outDir, progress, token);
            });
        }

        private static bool ReadTracksJob(string driveLetter, 
                                          IEnumerable<CdItemViewModel> tracks, 
                                          string outDir, 
                                          IProgress<long> progress,
                                          CancellationToken token)
        {
            using (var cd = new CdReader())
            {
                cd.Open(driveLetter[0]);

                bool prepare = cd.LoadCD() && cd.LockCD() && cd.Refresh();

                if (!prepare)
                    return false;

                foreach (var track in tracks)
                {
                    progress.Report(0);
                    var outfile = Path.Combine(outDir, $"Track {track.Track}.wav");
                    using (var file = File.Create(outfile))
                    {
                        bool result = cd.ReadTrack(track.Track, file, progress, token);

                        if (!result)
                            return false;
                    }
                }

                cd.UnLockCD();
            }

            return true;
        }
    }
}
