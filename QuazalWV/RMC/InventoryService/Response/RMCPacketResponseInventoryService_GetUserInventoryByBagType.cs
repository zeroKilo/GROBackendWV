using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseInventoryService_GetUserInventoryByBagType : RMCPResponse
    {
        public List<GR5_UserItem> items = new List<GR5_UserItem>();
        public List<GR5_InventoryBag> bags = new List<GR5_InventoryBag>();
        public List<GR5_WeaponConfiguration> weaponConfig = new List<GR5_WeaponConfiguration>();

        public RMCPacketResponseInventoryService_GetUserInventoryByBagType(byte bagType, byte offset)
        {
            items = DBHelper.GetUserItems(0x1234, (byte)(bagType + offset));
            bags = DBHelper.GetInventoryBags(0x1234, (byte)(bagType + offset));
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
