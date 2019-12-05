using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_LeaderboardRow
    {
        public uint m_PlayerID;
        public string m_Name;
        public uint m_AvatarPortraitID;
        public uint m_Level;
        public uint m_Rank;
        public uint m_RankedValue;
        public List<GR5_Statistic> m_StatisticsVector = new List<GR5_Statistic>();

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_PlayerID);
            Helper.WriteString(s, m_Name);
            Helper.WriteU32(s, m_AvatarPortraitID);
            Helper.WriteU32(s, m_Level);
            Helper.WriteU32(s, m_Rank);
            Helper.WriteU32(s, m_RankedValue);
            Helper.WriteU32(s, (uint)m_StatisticsVector.Count);
            foreach (GR5_Statistic st in m_StatisticsVector)
                st.toBuffer(s);
        }
    }
}
