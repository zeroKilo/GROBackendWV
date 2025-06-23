using System;
using System.IO;

namespace DDLParserWV
{
    public static class Utils
    {
        /// <summary>
        /// Magic number at the beginning of a DDL binary parse tree (BPT).
        /// </summary>
        public const uint BPT_MAGIC = 0xCD652312;

        public static string ReadString(Stream s)
        {
            string result = "";
            uint len = ReadU32(s);
            if (len > 1000)
                throw new Exception($"String length {len} at 0x{s.Position:X8}");
            for (int i = 0; i < len; i++)
                result += (char)s.ReadByte();
            return result;
        }

        public static uint ReadU32(Stream s)
        {
            uint result = 0;
            for (int i = 0; i < 4; i++)
            {
                result <<= 8;
                result |= (byte)s.ReadByte();
            }
            return result;
        }

        public static string MakeTabs(uint depth)
        {
            string tabs = "";
            for (int i = 0; i < depth; i++)
                tabs += "\t";
            return tabs;
        }
    }
}
