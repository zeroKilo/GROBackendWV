using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_LeaderboardReadResult
    {
        public uint m_LeaderboardID;
        public uint m_LeaderboardResetPeriodType;
        public uint m_LeaderboardRowCount;
        public List<GR5_LeaderboardRow> m_LeaderboardRowVector = new List<GR5_LeaderboardRow>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_LeaderboardID);
            Helper.WriteU32(s, m_LeaderboardResetPeriodType);
            Helper.WriteU32(s, m_LeaderboardRowCount);
            Helper.WriteU32(s, (uint)m_LeaderboardRowVector.Count);
            foreach (GR5_LeaderboardRow row in m_LeaderboardRowVector)
                row.toBuffer(s);
        }
    }
}
