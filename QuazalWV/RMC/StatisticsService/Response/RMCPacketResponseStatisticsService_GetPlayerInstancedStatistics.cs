using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStatisticsService_GetPlayerInstancedStatistics : RMCPResponse
    {
        public List<GR5_PlayerInstancedStatisticsBlock> instStats = new List<GR5_PlayerInstancedStatisticsBlock>();

        public RMCPacketResponseStatisticsService_GetPlayerInstancedStatistics()
        {
            GR5_PlayerInstancedStatisticsBlock b = new GR5_PlayerInstancedStatisticsBlock();
            b.m_StatisticVector.Add(new GR5_InstancedStatistic());
            instStats.Add(b);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)instStats.Count);
            foreach (GR5_PlayerInstancedStatisticsBlock p in instStats)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_GetPlayerInstancedStatistics]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
