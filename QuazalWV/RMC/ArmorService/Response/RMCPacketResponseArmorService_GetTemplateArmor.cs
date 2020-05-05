using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseArmorService_GetTemplateArmor : RMCPResponse
    {
        public List<GR5_ArmorTier> tiers = new List<GR5_ArmorTier>();
        public List<GR5_ArmorInsert> inserts = new List<GR5_ArmorInsert>();
        public List<GR5_ArmorItem> items = new List<GR5_ArmorItem>();

        public RMCPacketResponseArmorService_GetTemplateArmor()
        {
            tiers = DBHelper.GetArmorTiers();
            inserts = DBHelper.GetArmorInserts();
            items = DBHelper.GetArmorItems();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)tiers.Count);
            foreach (GR5_ArmorTier t in tiers)
                t.toBuffer(m);
            Helper.WriteU32(m, (uint)inserts.Count);
            foreach (GR5_ArmorInsert i in inserts)
                i.toBuffer(m);
            Helper.WriteU32(m, (uint)items.Count);
            foreach (GR5_ArmorItem i in items)
                i.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseArmorService_GetTemplateArmor]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
