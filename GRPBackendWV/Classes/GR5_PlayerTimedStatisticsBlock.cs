using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_PlayerTimedStatisticsBlock
    {
        public uint m_PlayerID;
        public List<GR5_TimedStatistic> m_StatisticVector = new List<GR5_TimedStatistic>(); 

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_PlayerID);
            Helper.WriteU32(s, (uint)m_StatisticVector.Count);
            foreach (GR5_TimedStatistic t in m_StatisticVector)
                t.toBuffer(s);
        }
    }
}
