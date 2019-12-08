using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStatisticsService_Method2 : RMCPacketReply
    {
        public List<GR5_PlayerStatisticsBlock> psb = new List<GR5_PlayerStatisticsBlock>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)psb.Count);
            foreach (GR5_PlayerStatisticsBlock p in psb)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_Method2]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
