using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStatisticsService_GetPlayerTimedStatistics : RMCPResponse
    {
        public List<GR5_PlayerTimedStatisticsBlock> timedStats = new List<GR5_PlayerTimedStatisticsBlock>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)timedStats.Count);
            foreach (GR5_PlayerTimedStatisticsBlock b in timedStats)
                b.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_GetPlayerTimedStatistics]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
