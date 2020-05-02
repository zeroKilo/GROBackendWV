using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_GetAllApplyItems : RMCPResponse
    {
        public List<GR5_ApplyItem> items = new List<GR5_ApplyItem>();

        public RMCPacketResponseInventoryService_GetAllApplyItems()
        {
            items.Add(new GR5_ApplyItem());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)items.Count);
            foreach (GR5_ApplyItem item in items)
                item.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_GetAllApplyItems]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
