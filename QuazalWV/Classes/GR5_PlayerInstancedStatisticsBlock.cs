using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_PlayerInstancedStatisticsBlock
    {
        public uint m_PlayerID;
        public List<GR5_InstancedStatistic> m_StatisticVector = new List<GR5_InstancedStatistic>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_PlayerID);
            Helper.WriteU32(s, (uint)m_StatisticVector.Count);
            foreach (GR5_InstancedStatistic i in m_StatisticVector)
                i.toBuffer(s);
        }
    }
}
