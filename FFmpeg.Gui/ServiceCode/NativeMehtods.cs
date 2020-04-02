using FFmpeg.Gui.Domain.Cd;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FFmpeg.Gui.ServiceCode
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(string lpFileName,
                                                       [MarshalAs(UnmanagedType.U4)]
                                                       FileAccess dwDesiredAccess,
                                                       [MarshalAs(UnmanagedType.U4)]
                                                       FileShare dwShareMode,
                                                       IntPtr lpSecurityAttributes,
                                                       [MarshalAs(UnmanagedType.U4)]
                                                       FileMode dwCreationDisposition,
                                                       [MarshalAs(UnmanagedType.U4)]
                                                       FileAttributes dwFlagsAndAttributes,
                                                       IntPtr hTemplateFile);

        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(SafeFileHandle hDevice,
                                                  EIOControlCode IoControlCode,
                                                  [MarshalAs(UnmanagedType.AsAny)]
                                                  [In] object InBuffer,
                                                  uint nInBufferSize,
                                                  [MarshalAs(UnmanagedType.AsAny)]
                                                  [Out] object OutBuffer,
                                                  uint nOutBufferSize,
                                                  ref uint pBytesReturned,
                                                  [In] ref System.Threading.NativeOverlapped Overlapped);
    }
}