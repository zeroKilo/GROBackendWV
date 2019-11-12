using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseServerInfo_Method1 : RMCPacketReply
    {
        public class Unknown1
        {
            public uint[] unk1 = new uint[9];
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
            }
        }

        public class Unknown2
        {
            public string unk1;
            public string unk2;
            public uint unk3;
            public void toBuffer(Stream s)
            {
                Helper.WriteString(s, unk1);
                Helper.WriteString(s, unk2);
                Helper.WriteU32(s, unk3);
            }
        }

        public Unknown1 unk1 = new Unknown1();
        public Unknown1 unk2 = new Unknown1();
        public Unknown2 unk3 = new Unknown2();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            unk1.toBuffer(m);
            unk2.toBuffer(m);
            unk3.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseServerInfo_Method1]";
        }
    }
}
