using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_AdServer
    {
        public uint unk1;
        public byte unk2;
        public string unk3;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU8(s, unk2);
            Helper.WriteString(s, unk3);
        }
    }
}
