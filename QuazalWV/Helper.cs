using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Ionic.Zlib;

namespace QuazalWV
{
    public static class Helper
    {
        public static Random rnd = new Random();

        public static ulong MakeTimestamp()
        {
            return (ulong)new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        }

        public static bool ReadBool(Stream s)
        {
            return s.ReadByte() != 0;
        }
        public static byte ReadU8(Stream s)
        {
            return (byte)s.ReadByte();
        }

        public static ushort ReadU16(Stream s)
        {
            return (ushort)((byte)s.ReadByte() | ((byte)s.ReadByte() << 8));
        }

        public static ushort ReadU16LE(Stream s)
        {
            return (ushort)(((byte)s.ReadByte() << 8) | (byte)s.ReadByte());
        }

        public static uint ReadU32(Stream s)
        {
            return (uint)((byte)s.ReadByte() | 
                         ((byte)s.ReadByte() << 8) | 
                         ((byte)s.ReadByte() << 16) | 
                         ((byte)s.ReadByte() << 24));
        }

        public static ulong ReadU64(Stream s)
        {
            return (ulong)((byte)s.ReadByte() | 
                          ((byte)s.ReadByte() << 8) | 
                          ((byte)s.ReadByte() << 16) |
                          ((byte)s.ReadByte() << 24) |
                          ((byte)s.ReadByte() << 32) |
                          ((byte)s.ReadByte() << 40) |
                          ((byte)s.ReadByte() << 48) |
                          ((byte)s.ReadByte() << 56));
        }

        public static float ReadFloat(Stream s)
        {
            byte[] b = new byte[4];
            s.Read(b, 0, 4);
            return BitConverter.ToSingle(b, 0);
        }

        public static double ReadDouble(Stream s)
        {
            byte[] b = new byte[8];
            s.Read(b, 0, 8);
            return BitConverter.ToDouble(b, 0);
        }

        public static string ReadString(Stream s)
        {
            string result = "";
            ushort len = ReadU16(s);
            for (int i = 0; i < len - 1; i++)
                result += (char)s.ReadByte();
            s.ReadByte();
            return result;
        }

        public static List<string> ReadStringList(Stream s)
        {
            uint count = ReadU32(s);
            List<string> list = new List<string>();
            for (int i = 0; i < count; i++)
                list.Add(ReadString(s));
            return list;
        }

        public static void WriteU8(Stream s, byte v)
        {
            s.WriteByte(v);
        }

        public static void WriteBool(Stream s, bool v)
        {
            s.WriteByte((byte)(v ? 1 : 0));
        }

        public static void WriteU16(Stream s, ushort v)
        {
            s.WriteByte((byte)v);
            s.WriteByte((byte)(v >> 8));
        }

        public static void WriteU32(Stream s, uint v)
        {
            s.WriteByte((byte)v);
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)(v >> 16));
            s.WriteByte((byte)(v >> 24));
        }

        public static void WriteU16LE(Stream s, ushort v)
        {
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)v);
        }

        public static void WriteU32LE(Stream s, uint v)
        {
            s.WriteByte((byte)(v >> 24));
            s.WriteByte((byte)(v >> 16));
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)v);
        }

        public static void WriteU64(Stream s, ulong v)
        {
            s.WriteByte((byte)v);
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)(v >> 16));
            s.WriteByte((byte)(v >> 24));
            s.WriteByte((byte)(v >> 32));
            s.WriteByte((byte)(v >> 40));
            s.WriteByte((byte)(v >> 48));
            s.WriteByte((byte)(v >> 56));
        }

        public static void WriteFloat(Stream s, float v)
        {
            byte[] b = BitConverter.GetBytes(v);
            s.Write(b, 0, 4);
        }

        public static void WriteFloatLE(Stream s, float v)
        {
            byte[] b = BitConverter.GetBytes(v);
            s.WriteByte(b[3]);
            s.WriteByte(b[2]);
            s.WriteByte(b[1]);
            s.WriteByte(b[0]);
        }

        public static void WriteDouble(Stream s, double v)
        {
            byte[] b = BitConverter.GetBytes(v);
            s.Write(b, 0, 8);
        }

        public static void WriteString(Stream s, string v)
        {
            if (v != null)
            {
                WriteU16(s, (ushort)(v.Length + 1));
                foreach (char c in v)
                    s.WriteByte((byte)c);
                s.WriteByte(0);
            }
            else
            {
                s.WriteByte(1);
                s.WriteByte(0);
                s.WriteByte(0);
            }
        }

        public static void WriteStringList(Stream s, List<string> v)
        {
            WriteU32(s, (uint)v.Count());
            foreach(var entry in v)
                WriteString(s, entry);
        }

        public static byte[] Decompress(byte[] data)
        {
            ZlibStream s = new ZlibStream(new MemoryStream(data), Ionic.Zlib.CompressionMode.Decompress);
            MemoryStream result = new MemoryStream();
            s.CopyTo(result);
            return result.ToArray();
        }

        public static byte[] Compress(byte[] data)
        {
            ZlibStream s = new ZlibStream(new MemoryStream(data), Ionic.Zlib.CompressionMode.Compress);
            MemoryStream result = new MemoryStream();
            s.CopyTo(result);
            return result.ToArray();
        }

        public static byte[] Encrypt(string key, byte[] data)
        {
            return Encrypt(Encoding.ASCII.GetBytes(key), data);
        }

        public static byte[] Decrypt(string key, byte[] data)
        {
            return Encrypt(Encoding.ASCII.GetBytes(key), data);
        }

        public static byte[] Encrypt(byte[] key, byte[] data)
        {
            return EncryptOutput(key, data).ToArray();
        }

        public static byte[] Decrypt(byte[] key, byte[] data)
        {
            return EncryptOutput(key, data).ToArray();
        }

        private static byte[] EncryptInitalize(byte[] key)
        {
            byte[] s = Enumerable.Range(0, 256)
              .Select(i => (byte)i)
              .ToArray();
            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + key[i % key.Length] + s[i]) & 255;

                Swap(s, i, j);
            }
            return s;
        }

        private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
        {
            byte[] s = EncryptInitalize(key);
            int i = 0;
            int j = 0;
            return data.Select((b) =>
            {
                i = (i + 1) & 255;
                j = (j + s[i]) & 255;
                Swap(s, i, j);
                return (byte)(b ^ s[(s[i] + s[j]) & 255]);
            });
        }

        private static void Swap(byte[] s, int i, int j)
        {
            byte c = s[i];
            s[i] = s[j];
            s[j] = c;
        }

        public static byte[] DeriveKey(uint pid, string input = "UbiDummyPwd")
        {
            uint count = 65000 + (pid % 1024);
            MD5 md5 = MD5.Create();
            byte[] buff = Encoding.ASCII.GetBytes(input);
            for (uint i = 0; i < count; i++)
                buff = md5.ComputeHash(buff);
            return buff;
        }

        public static byte[] MakeHMAC(byte[] key, byte[] data)
        {
            HMACMD5 hmac = new HMACMD5(key);
            return hmac.ComputeHash(data);
        }

        public static byte[] MakeFilledArray(int len)
        {
            byte[] result = new byte[len];
            for (int i = 0; i < len; i++)
                result[i] = (byte)i;
            return result;
        }
    }
}
