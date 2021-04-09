using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStatisticsService_GetDesignerStatistics : RMCPResponse
    {
        public List<GR5_DesignerStatistics> designerStats = new List<GR5_DesignerStatistics>();

        public RMCPacketResponseStatisticsService_GetDesignerStatistics()
        {
            designerStats.Add(new GR5_DesignerStatistics());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)designerStats.Count);
            foreach (GR5_DesignerStatistics ds in designerStats)
                ds.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_GetDesignerStatistics]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
