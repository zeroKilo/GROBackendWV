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
        public string content;
        public ulong date;
        public uint unk7;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU32(s, unk2);
            Helper.WriteU32(s, unk3);
            Helper.WriteU32(s, unk4);
            Helper.WriteString(s, content);
            Helper.WriteU64(s, date);
            Helper.WriteU32(s, unk7);
        }
    }
}
