using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStatisticsService_Method4 : RMCPResponse
    {
        public List<GR5_PlayerTimedStatisticsBlock> list = new List<GR5_PlayerTimedStatisticsBlock>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_PlayerTimedStatisticsBlock b in list)
                b.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_Method4]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
