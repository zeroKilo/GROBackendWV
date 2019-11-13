using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseOpsProtocolService_GetAllPriorityBroadcasts: RMCPacketReply
    {
        public class PriorityBroadcast
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

        public List<PriorityBroadcast> pbs = new List<PriorityBroadcast>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)pbs.Count);
            foreach (PriorityBroadcast pb in pbs)
                pb.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketTesponseOpsProtocolService_GetAllPriorityBroadcasts]";
        }
    }
}
