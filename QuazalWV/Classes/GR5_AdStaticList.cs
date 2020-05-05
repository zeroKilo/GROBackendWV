using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_AdStaticList
    {
        public uint unk1;
        public uint unk2;
        public byte unk3;
        public byte unk4;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU32(s, unk2);
            Helper.WriteU8(s, unk3);
            Helper.WriteU8(s, unk4);
        }
    }
}
