using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_GetUserInventoryByBagType : RMCPacketReply
    {
        public List<GR5_UserItem> items = new List<GR5_UserItem>();
        public List<GR5_InventoryBag> bags = new List<GR5_InventoryBag>();
        public List<GR5_WeaponConfiguration> weaponConfig = new List<GR5_WeaponConfiguration>();

        public RMCPacketResponseInventoryService_GetUserInventoryByBagType(byte bagType, byte offset)
        {
            GR5_InventoryBag b = new GR5_InventoryBag();
            b.m_PersonaID = 0x1234;
            b.m_InventoryBagType = (uint)(bagType + offset);
            for (uint i = 7; i < 10; i++)
            {
                GR5_InventoryBagSlot slot = new GR5_InventoryBagSlot();
                slot.Durability = 99;
                slot.InventoryID = i;
                slot.SlotID = i;
                b.m_InventoryBagSlotVector.Add(slot);
                GR5_UserItem item = new GR5_UserItem();
                item.ItemID = 0x7777;
                item.InventoryID = i;
                item.ItemType = (byte)(bagType + offset);
                item.PersonaID = 0x1234;
                items.Add(item);
            }
            bags.Add(b);
            weaponConfig.Add(new GR5_WeaponConfiguration());
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
            Helper.WriteU32(m, (uint)weaponConfig.Count);
            foreach (GR5_WeaponConfiguration u in weaponConfig)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_GetUserInventoryByBagType]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
