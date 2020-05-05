using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStatisticsService_Method1 : RMCPResponse
    {
        public List<GR5_DesignerStatistics> list = new List<GR5_DesignerStatistics>();

        public RMCPacketResponseStatisticsService_Method1()
        {
            list.Add(new GR5_DesignerStatistics());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_DesignerStatistics ds in list)
                ds.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_Method1]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
