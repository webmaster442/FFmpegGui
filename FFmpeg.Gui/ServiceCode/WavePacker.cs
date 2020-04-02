//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Domain.Cd;
using System.IO;

namespace FFmpeg.Gui.ServiceCode
{
    public static class WavePacker
    {
        private const uint WaveHeaderSize = 38;
        private const uint WaveFormatSize = 18;

        private static byte[] Int2ByteArr(uint val)
        {
            byte[] res = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                res[i] = (byte)(val >> (i * 8));
            }
            return res;
        }

        private static byte[] Int2ByteArr(short val)
        {
            byte[] res = new byte[2];
            for (int i = 0; i < 2; i++)
            {
                res[i] = (byte)(val >> (i * 8));
            }
            return res;
        }

        private static void Write(Stream target, byte[] data)
        {
            target.Write(data, 0, data.Length);
        }

        public static void WriteHeader(Stream target, WaveFormat m_InputDataFormat, uint m_AudioDataSize)
        {
            Write(target, new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' });
            Write(target, Int2ByteArr(m_AudioDataSize + WaveHeaderSize));
            Write(target, new byte[] { (byte)'W', (byte)'A', (byte)'V', (byte)'E' });
            Write(target, new byte[] { (byte)'f', (byte)'m', (byte)'t', (byte)' ' });
            Write(target, Int2ByteArr(WaveFormatSize));
            Write(target, Int2ByteArr(m_InputDataFormat.wFormatTag));
            Write(target, Int2ByteArr(m_InputDataFormat.nChannels));
            Write(target, Int2ByteArr((uint)m_InputDataFormat.nSamplesPerSec));
            Write(target, Int2ByteArr((uint)m_InputDataFormat.nAvgBytesPerSec));
            Write(target, Int2ByteArr(m_InputDataFormat.nBlockAlign));
            Write(target, Int2ByteArr(m_InputDataFormat.wBitsPerSample));
            Write(target, Int2ByteArr(m_InputDataFormat.cbSize));
            Write(target, new byte[] { (byte)'d', (byte)'a', (byte)'t', (byte)'a' });
            Write(target, Int2ByteArr(m_AudioDataSize));
        }
    }
}
