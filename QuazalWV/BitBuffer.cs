using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class BitBuffer
    {
        MemoryStream buffer;
        public int byteSize;
        public int bitSize;
        public int bitPos;

        public BitBuffer()
        {
            buffer = new MemoryStream();
            buffer.WriteByte(0);
            byteSize = 0;
            bitSize = 0;
            bitPos = 0;
        }

        public byte[] toArray()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, (byte)(byteSize * 8 - bitSize));
            byte[] data = buffer.ToArray();
            m.Write(data, 0, data.Length);
            return m.ToArray();
        }

        public void WriteBit(bool v)
        {
            int bytePos = bitPos / 8;
            int bitOffset = bitPos & 7;
            if ((bitPos % 8) == 0 && bitPos / 8 == byteSize)
            {
                buffer.Seek(bytePos, 0);
                buffer.WriteByte(0);
                byteSize++;
            }
            bitPos++;
            if (bitPos > bitSize)
                bitSize = bitPos;
            buffer.Seek(bytePos, 0);
            byte b = (byte)buffer.ReadByte();
            if (v == true)
                b |= (byte)(1 << bitOffset);
            else
                b &= (byte)~(1 << bitOffset);
            buffer.Seek(bytePos, 0);
            buffer.WriteByte(b);
        }

        public void WriteBits(uint value, int n)
        {
            for (int i = 0; i < n; i++)
            {
                WriteBit((value & 1) == 1);
                value >>= 1;
            }
        }

        public bool ReadBit()
        {
            if (bitPos >= bitSize)
                throw new Exception("End of bitbuffer reached");
            int bytePos = bitPos / 8;
            int bitOffset = bitPos & 7;
            bitPos++;
            buffer.Seek(bytePos, 0);
            byte b = (byte)buffer.ReadByte();
            return ((b >> bitOffset) & 1) == 1;
        }

        public uint ReadBits(int n)
        {
            uint result = 0;
            for (int i = 0; i < n; i++)
                if (ReadBit())
                    result |= (uint)(1 << i);
            return result;
        }
    }
}
