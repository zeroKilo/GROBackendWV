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
        public class LootItem
        {
            public uint mID;
            public uint mLootID;
            public uint mLootItemSku;
            public uint mPercentage;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, mID);
                Helper.WriteU32(s, mLootID);
                Helper.WriteU32(s, mLootItemSku);
                Helper.WriteU32(s, mPercentage);
            }
        }

        public List<LootItem> items = new List<LootItem>();

        public RMCPacketResponseLootService_GetLootStatic()
        {
            items.Add(new LootItem());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)items.Count);
            foreach (LootItem item in items)
                item.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLootService_GetLootStatic]";
        }
    }
}
