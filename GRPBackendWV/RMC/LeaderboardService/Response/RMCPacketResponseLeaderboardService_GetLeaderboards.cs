using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseLeaderboardService_GetLeaderboards : RMCPacketReply
    {
        public List<GR5_Leaderboard> boards = new List<GR5_Leaderboard>();

        public RMCPacketResponseLeaderboardService_GetLeaderboards()
        {
            GR5_Leaderboard lb = new GR5_Leaderboard();
            lb.m_OasisDescriptionID = lb.m_OasisNameID = 70870;
            boards.Add(lb);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)boards.Count);
            foreach (GR5_Leaderboard b in boards)
                b.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLeaderboardService_GetLeaderboards]";
        }
    }
}
