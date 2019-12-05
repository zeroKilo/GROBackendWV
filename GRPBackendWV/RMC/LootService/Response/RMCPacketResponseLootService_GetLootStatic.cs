using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseLootService_GetLootStatic : RMCPacketReply
    {
        public List<GR5_LootItem> items = new List<GR5_LootItem>();

        public RMCPacketResponseLootService_GetLootStatic()
        {
            items.Add(new GR5_LootItem());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)items.Count);
            foreach (GR5_LootItem item in items)
                item.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLootService_GetLootStatic]";
        }
    }
}
