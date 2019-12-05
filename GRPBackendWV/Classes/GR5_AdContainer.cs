using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_AdContainer
    {
        public uint unk1;
        public uint unk2;
        public string unk3;
        public byte unk4;
        public byte unk5;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU32(s, unk2);
            Helper.WriteString(s, unk3);
            Helper.WriteU8(s, unk4);
            Helper.WriteU8(s, unk5);
        }
    }
}
