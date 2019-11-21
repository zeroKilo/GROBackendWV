using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseLootService_Method4 : RMCPacketReply
    {
        public class unknown
        {
            public uint unk1;
            public uint unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk1);
            }
        }

        public List<unknown> list = new List<unknown>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (unknown u in list)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLootService_Method4]";
        }
    }
}
