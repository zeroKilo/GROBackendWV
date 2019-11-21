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
        public class UserItem
        {
            public uint InventoryID;
            public uint PersonaID;
            public byte ItemType;
            public uint ItemID;
            public uint OasisName;
            public float IGCPrice;
            public float GRCashPrice;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, InventoryID);
                Helper.WriteU32(s, PersonaID);
                Helper.WriteU8(s, ItemType);
                Helper.WriteU32(s, ItemID);
                Helper.WriteU32(s, OasisName);
                Helper.WriteFloat(s, IGCPrice);
                Helper.WriteFloat(s, GRCashPrice);
            }
        }
        public class InventoryBagSlot
        {
            public uint InventoryID;
            public uint SlotID;
            public uint Durability;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, InventoryID);
                Helper.WriteU32(s, SlotID);
                Helper.WriteU32(s, Durability);
            }
        }
        public class InventoryBag
        {
            public uint m_PersonaID;
            public uint m_InventoryBagType;
            public List<InventoryBagSlot> m_InventoryBagSlotVector = new List<InventoryBagSlot>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_PersonaID);
                Helper.WriteU32(s, m_InventoryBagType);
                Helper.WriteU32(s, (uint)m_InventoryBagSlotVector.Count);
                foreach (InventoryBagSlot c in m_InventoryBagSlotVector)
                    c.toBuffer(s);
            }
        }
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
        public List<UserItem> items = new List<UserItem>();
        public List<InventoryBag> bags = new List<InventoryBag>();
        public List<unknown> unk1 = new List<unknown>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)items.Count);
            foreach (UserItem c in items)
                c.toBuffer(m);
            Helper.WriteU32(m, (uint)bags.Count);
            foreach (InventoryBag c in bags)
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
