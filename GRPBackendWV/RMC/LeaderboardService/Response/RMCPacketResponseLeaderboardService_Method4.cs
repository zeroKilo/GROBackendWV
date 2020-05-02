using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseLeaderboardService_Method4 : RMCPResponse
    {
        public List<GR5_LeaderboardReadResult> list = new List<GR5_LeaderboardReadResult>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_LeaderboardReadResult r in list)
                r.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLeaderboardService_Method4]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
