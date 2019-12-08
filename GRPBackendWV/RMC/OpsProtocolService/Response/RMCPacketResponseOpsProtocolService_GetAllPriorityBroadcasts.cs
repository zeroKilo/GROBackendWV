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
        public List<GR5_PriorityBroadcast> pbs = new List<GR5_PriorityBroadcast>();

        public RMCPacketResponseOpsProtocolService_GetAllPriorityBroadcasts()
        {
            pbs.Add(new GR5_PriorityBroadcast());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)pbs.Count);
            foreach (GR5_PriorityBroadcast pb in pbs)
                pb.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseOpsProtocolService_GetAllPriorityBroadcasts]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
