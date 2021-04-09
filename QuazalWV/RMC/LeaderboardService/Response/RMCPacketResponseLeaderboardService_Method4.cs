using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseLeaderboardService_ReadLeaderBoardStatsForRank : RMCPResponse
    {
        public List<GR5_LeaderboardReadResult> readResults = new List<GR5_LeaderboardReadResult>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)readResults.Count);
            foreach (GR5_LeaderboardReadResult r in readResults)
                r.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLeaderboardService_ReadLeaderBoardStatsForRank]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
