using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInboxMessageService_Method1 : RMCPacketReply
    {
        public class Unknown1
        {
            public uint unk1;
            public uint unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
            }
        }

        public List<Unknown1> unk1 = new List<Unknown1>();

        public RMCPacketResponseInboxMessageService_Method1()
        {
            unk1.Add(new Unknown1());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (Unknown1 u in unk1)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInboxMessageService_Method1]";
        }
    }
}
