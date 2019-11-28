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
        public class Leaderboard
        {
            public uint m_ID;
            public string m_Name;
            public uint m_ClassID;
            public uint m_OasisNameID;
            public uint m_OasisDescriptionID;
            public uint m_Flags;
            public uint m_DesignerStatisticID;
            public uint m_SortOrder;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ID);
                Helper.WriteString(s, m_Name);
                Helper.WriteU32(s, m_ClassID);
                Helper.WriteU32(s, m_OasisNameID);
                Helper.WriteU32(s, m_OasisDescriptionID);
                Helper.WriteU32(s, m_Flags);
                Helper.WriteU32(s, m_DesignerStatisticID);
                Helper.WriteU32(s, m_SortOrder);
            }
        }

        public List<Leaderboard> boards = new List<Leaderboard>();

        public RMCPacketResponseLeaderboardService_GetLeaderboards()
        {
            Leaderboard lb = new Leaderboard();
            lb.m_OasisDescriptionID = lb.m_OasisNameID = 70870;
            boards.Add(lb);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)boards.Count);
            foreach (Leaderboard b in boards)
                b.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLeaderboardService_GetLeaderboards]";
        }
    }
}
