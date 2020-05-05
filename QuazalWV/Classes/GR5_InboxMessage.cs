using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_InboxMessage
    {
        public uint unk1;
        public uint unk2;
        public uint unk3;
        public uint unk4;
        public string unk5;
        public ulong unk6;
        public uint unk7;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU32(s, unk2);
            Helper.WriteU32(s, unk3);
            Helper.WriteU32(s, unk4);
            Helper.WriteString(s, unk5);
            Helper.WriteU64(s, unk6);
            Helper.WriteU32(s, unk7);
        }
    }
}
