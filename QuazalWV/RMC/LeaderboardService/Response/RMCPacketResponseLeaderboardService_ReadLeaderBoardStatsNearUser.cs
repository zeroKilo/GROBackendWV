using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseLeaderboardService_ReadLeaderBoardStatsNearUser : RMCPResponse
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
            return "[RMCPacketResponseLeaderboardService_ReadLeaderBoardStatsNearUser]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
