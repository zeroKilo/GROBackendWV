using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_PlayerStatisticsBlock
    {
        public uint m_PlayerID;
        public List<GR5_Statistic> m_StatisticVector = new List<GR5_Statistic>();

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_PlayerID);
            Helper.WriteU32(s, (uint)m_StatisticVector.Count);
            foreach (GR5_Statistic st in m_StatisticVector)
                st.toBuffer(s);
        }
    }
}
