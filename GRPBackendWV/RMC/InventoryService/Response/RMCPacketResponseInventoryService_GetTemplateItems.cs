using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_GetTemplateItems : RMCPResponse
    {
        public List<GR5_TemplateItem> items = new List<GR5_TemplateItem>();

        public RMCPacketResponseInventoryService_GetTemplateItems()
        {
            items = DBHelper.GetTemplateItems();
        }
        
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)items.Count);
            foreach (GR5_TemplateItem item in items)
                item.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_GetTemplateItems]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
