using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class BitFieldU32
    {
        public class BitFieldEntry
        {
            public byte start;
            public byte size;
            public string name;
            public uint word;

            public BitFieldEntry(byte s, byte l, string n, uint field = 0)
            {
                start = s;
                size = l;
                name = n;
                word = ExtractValue(field);
            }

            public uint ExtractValue(uint field)
            {
                int a = (32 - start - size);
                int b = (32 - size);
                uint tmp = field << a;
                tmp >>= b;
                return tmp;
            }

            public uint InsertValue(uint field)
            {
                return InsertValue(field, word);
            }

            public uint InsertValue(uint field, uint value)
            {
                int a = (32 - start - size);
                int b = (32 - size);
                uint mask = (0xFFFFFFFF << a);
                mask >>= b;
                uint tmp = value & mask;
                mask <<= start;
                mask = ~mask;
                tmp <<= start;
                return (field & mask) | tmp;
            }
        }

        public List<BitFieldEntry> entries = new List<BitFieldEntry>();

        public BitFieldU32(List<BitFieldEntry> e, uint data = 0)
        {
            entries = e;
            if (e == null)
                return;
            Update(data);
        }

        public void Update(uint data)
        {
            foreach (BitFieldEntry entry in entries)
                entry.word = entry.ExtractValue(data);
        }

        public uint ToU32()
        {
            uint tmp = 0;
            foreach (BitFieldEntry entry in entries)
                tmp = entry.InsertValue(tmp);
            return tmp;
        }
    }
}
