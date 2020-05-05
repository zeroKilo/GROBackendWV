using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_PriorityBroadcast
    {
        public uint unk1;
        public string unk2;
        public uint unk3;
        public uint unk4;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteString(s, unk2);
            Helper.WriteU32(s, unk3);
            Helper.WriteU32(s, unk4);
        }
    }
}
