using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStatisticsService_Method3 : RMCPResponse
    {
        public List<GR5_PlayerInstancedStatisticsBlock> list = new List<GR5_PlayerInstancedStatisticsBlock>();

        public RMCPacketResponseStatisticsService_Method3()
        {
            GR5_PlayerInstancedStatisticsBlock b = new GR5_PlayerInstancedStatisticsBlock();
            b.m_StatisticVector.Add(new GR5_InstancedStatistic());
            list.Add(b);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_PlayerInstancedStatisticsBlock p in list)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_Method3]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
