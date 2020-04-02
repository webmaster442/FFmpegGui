//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain.Cd;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace FFmpeg.Gui.ServiceCode
{
    public sealed class CdReader : IDisposable
    {
        private CdromTOC _Toc;
        private char _driveLetter;
        private bool _tocIsValid;
        private SafeFileHandle _driveHandle;

        public CdReader()
        {
            _driveLetter = '\0';
            _Toc = new CdromTOC();
        }

        public bool Open(char drive)
        {
            Close();
            var info = new DriveInfo(drive.ToString());
            if (info.DriveType != DriveType.CDRom) return false;

            _driveHandle = NativeMethods.CreateFile($"\\\\.\\{drive}:",
                                                    FileAccess.Read,
                                                    FileShare.Read,
                                                    IntPtr.Zero,
                                                    FileMode.Open,
                                                    0,
                                                    IntPtr.Zero);

            if (_driveHandle?.IsInvalid == false)
            {
                _driveLetter = drive;
                return true;
            }

            return false;
        }

        public void Close()
        {
            UnLockCD();
            if (_driveHandle?.IsClosed == false)
            {
                _driveHandle.Close();
                _driveHandle = null;
            }
            _driveLetter = '\0';
            _tocIsValid = false;
        }

        public bool LockCD()
        {
            return ChangeState(true);
        }

        public void UnLockCD()
        {
            ChangeState(false);
        }

        public bool LoadCD()
        {
            _tocIsValid = false;
            if (_driveHandle?.IsInvalid == false)
            {
                uint Dummy = 0;
                NativeOverlapped overlap = default;
                return NativeMethods.DeviceIoControl(_driveHandle,
                                                     EIOControlCode.StorageLoadMedia,
                                                     null,
                                                     0,
                                                     null,
                                                     0,
                                                     ref Dummy,
                                                     ref overlap);
            }

            return false;
        }

        public bool EjectCD()
        {
            _tocIsValid = false;
            if (_driveHandle?.IsInvalid == false)
            {
                uint Dummy = 0;
                NativeOverlapped overlap = default;
                return NativeMethods.DeviceIoControl(_driveHandle,
                                                     EIOControlCode.StorageEjectMedia,
                                                     null,
                                                     0,
                                                     null,
                                                     0,
                                                     ref Dummy,
                                                     ref overlap);
            }

            return false;
        }

        public bool IsCDReady
        {
            get
            {
                if (_driveHandle?.IsInvalid == false)
                {
                    uint Dummy = 0;
                    NativeOverlapped overlap = default;
                    bool result = NativeMethods.DeviceIoControl(_driveHandle,
                                                                EIOControlCode.StorageCheckVerify,
                                                                null,
                                                                0,
                                                                null,
                                                                0,
                                                                ref Dummy,
                                                                ref overlap);
                    if (!result)
                        _tocIsValid = false;

                    return result;
                }
                _tocIsValid = false;
                return false;
            }
        }

        public int NumberOfTracks
        {
            get
            {
                if (_tocIsValid)
                    return _Toc.LastTrack - _Toc.FirstTrack + 1;
                else
                    return -1;
            }
        }

        public int NumberOfAudioTracks
        {
            get
            {
                if (!_tocIsValid) return -1;

                int tracks = 0;
                for (int i = _Toc.FirstTrack - 1; i < _Toc.LastTrack; i++)
                {
                    if (_Toc.TrackData[i].Control == 0)
                        tracks++;
                }
                return tracks;
            }
        }

        public CdTrackInfo GetTrackInfo(int track)
        {
            if (_tocIsValid &&
                (track >= _Toc.FirstTrack) &&
                (track <= _Toc.LastTrack))
            {

                int StartSect = GetStartSector(track);
                int EndSect = GetEndSector(track);

                CdTrackInfo ret = new CdTrackInfo
                {
                    IsAudio = _Toc.TrackData[track].Control == 0,
                    Size = (uint)(EndSect - StartSect) * CdConstants.CB_AUDIO
                };
                ret.Length = ret.Size / 176400.0;
                return ret;

            }
            return null;
        }

        public bool Refresh()
        {
            if (IsCDReady)
                return ReadTOC();
            else
                return false;
        }

        public bool ReadTrack(int track, Stream target, IProgress<uint> BytesReadReport)
        {
            if (_tocIsValid &&
                (track >= _Toc.FirstTrack) &&
                (track <= _Toc.LastTrack) &&
                (target != null))
            {
                int StartSect = GetStartSector(track);
                int EndSect = GetEndSector(track);

                uint Bytes2Read = (uint)(EndSect - StartSect) * CdConstants.CB_AUDIO;
                uint BytesRead = 0;
                byte[] Data = new byte[CdConstants.CB_AUDIO * CdConstants.NSECTORS];
                bool Cont = true;
                bool ReadOk = true;

                BytesReadReport?.Report(0);

                WavePacker.WriteHeader(target, new WaveFormat(44100, 16, 2), Bytes2Read);

                for (int sector = StartSect; (sector < EndSect) && (Cont) && (ReadOk); sector += CdConstants.NSECTORS)
                {
                    int Sectors2Read = ((sector + CdConstants.NSECTORS) < EndSect) ? CdConstants.NSECTORS : (EndSect - sector);
                    ReadOk = ReadSector(sector, Data, Sectors2Read);
                    if (ReadOk)
                    {
                        int read = (CdConstants.CB_AUDIO * Sectors2Read);
                        target.Write(Data, 0, read);
                        BytesRead += (uint)read;
                        BytesReadReport?.Report(BytesRead);
                    }
                }

                return ReadOk;

            }

            return false;
        }

        public void Dispose()
        {
            Close();
        }

        #region Private

        private bool ChangeState(bool lockDrive)
        {
            if (_driveHandle?.IsInvalid == false)
            {
                uint Dummy = 0;
                var nativeOverlapped = new NativeOverlapped();
                var preventRemoval = new PreventMediaRemovalT();
                if (lockDrive)
                    preventRemoval.PreventMediaRemoval = 1;
                else
                    preventRemoval.PreventMediaRemoval = 0;
                return NativeMethods.DeviceIoControl(_driveHandle,
                                                     EIOControlCode.StorageMediaRemoval,
                                                     preventRemoval,
                                                     (uint)Marshal.SizeOf(preventRemoval),
                                                     null,
                                                     0,
                                                     ref Dummy,
                                                     ref nativeOverlapped);
            }

            return false;
        }

        private bool ReadTOC()
        {
            if (_driveHandle?.IsInvalid == false)
            {
                uint BytesRead = 0;
                NativeOverlapped overlaped = default;
                _tocIsValid = NativeMethods.DeviceIoControl(_driveHandle,
                                                            EIOControlCode.CDromReadTOC,
                                                            null,
                                                            0,
                                                            _Toc,
                                                            (uint)Marshal.SizeOf(_Toc),
                                                            ref BytesRead,
                                                            ref overlaped);
            }
            else
                _tocIsValid = false;

            return _tocIsValid;
        }

        private int GetStartSector(int track)
        {
            if (_tocIsValid
                && (track >= _Toc.FirstTrack)
                && (track <= _Toc.LastTrack))
            {
                TrackData td = _Toc.TrackData[track - 1];
                return (td.Address_1 * 60 * 75 + td.Address_2 * 75 + td.Address_3) - 150;
            }
            else
            {
                return -1;
            }
        }

        private int GetEndSector(int track)
        {
            if (_tocIsValid 
                && (track >= _Toc.FirstTrack)
                && (track <= _Toc.LastTrack))
            {
                TrackData td = _Toc.TrackData[track];
                return (td.Address_1 * 60 * 75 + td.Address_2 * 75 + td.Address_3) - 151;
            }
            else
            {
                return -1;
            }
        }

        private bool ReadSector(int sector, byte[] Buffer, int NumSectors)
        {
            if (_tocIsValid &&
                ((sector + NumSectors) <= GetEndSector(_Toc.LastTrack)) &&
                (Buffer.Length >= CdConstants.CB_AUDIO * NumSectors))
            {
                RawReadInfo rri = new RawReadInfo();
                rri.TrackMode = TrackModeType.CDDA;
                rri.SectorCount = (uint)NumSectors;
                rri.DiskOffset = sector * CdConstants.CB_CDROMSECTOR;

                uint BytesRead = 0;

                NativeOverlapped overlaped = default;
                return NativeMethods.DeviceIoControl(_driveHandle,
                                                     EIOControlCode.CDromRawRead,
                                                     rri,
                                                     (uint)Marshal.SizeOf(rri),
                                                     Buffer,
                                                     (uint)NumSectors * CdConstants.CB_AUDIO,
                                                     ref BytesRead,
                                                     ref overlaped);
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
