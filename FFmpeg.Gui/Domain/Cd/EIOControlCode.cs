//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.IO;

namespace FFmpeg.Gui.Domain.Cd
{
    [Flags]
    internal enum EIOControlCode : uint
    {
        StorageMediaRemoval = (EFileDevice.MassStorage << 16) | (0x0201 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
        StorageLoadMedia = (EFileDevice.MassStorage << 16) | (0x0203 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
        StorageEjectMedia = (EFileDevice.MassStorage << 16) | (0x0202 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
        StorageCheckVerify = (EFileDevice.MassStorage << 16) | (0x0200 << 2) | EMethod.Buffered | (FileAccess.Read << 14),
        CDromReadTOC = 0x00024000,
        CDromRawRead = 0x0002403E,
    }
}
