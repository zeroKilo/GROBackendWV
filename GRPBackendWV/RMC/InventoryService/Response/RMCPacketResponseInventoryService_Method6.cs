using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_Method6 : RMCPacketReply
    {
        public class unknown
        {
            public uint unk1;
            public List<uint> unk2 = new List<uint>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, (uint)unk2.Count);
                foreach (uint u in unk2)
                    Helper.WriteU32(s, u);
            }
        }
        public List<GR5_UserItem> items = new List<GR5_UserItem>();
        public List<GR5_InventoryBag> bags = new List<GR5_InventoryBag>();
        public List<unknown> unk1 = new List<unknown>();

        public RMCPacketResponseInventoryService_Method6()
        {
            items.Add(new GR5_UserItem());
            GR5_InventoryBag b = new GR5_InventoryBag();
            b.m_InventoryBagSlotVector.Add(new GR5_InventoryBagSlot());
            bags.Add(b);
            unk1.Add(new unknown());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)items.Count);
            foreach (GR5_UserItem c in items)
                c.toBuffer(m);
            Helper.WriteU32(m, (uint)bags.Count);
            foreach (GR5_InventoryBag c in bags)
                c.toBuffer(m);
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (unknown u in unk1)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_Method6]";
        }
    }
}
