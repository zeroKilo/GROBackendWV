using System.Collections.Generic;
using System.IO;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_CompleteBuyArmourAndAttachInserts : RMCPResponse
    {
        public List<GR5_UserItem> Inventory { get; set; }
        public List<GR5_PersonaArmorTier> PersonaArmorTiers { get; set; }

        public RMCPacketResponseStoreService_CompleteBuyArmourAndAttachInserts()
        {
            Inventory = new List<GR5_UserItem>();
            PersonaArmorTiers = new List<GR5_PersonaArmorTier>();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();

            Helper.WriteU32(m, (uint)Inventory.Count);
            foreach (GR5_UserItem item in Inventory)
                item.toBuffer(m);

            Helper.WriteU32(m, (uint)PersonaArmorTiers.Count);
            foreach (GR5_PersonaArmorTier pat in PersonaArmorTiers)
                pat.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[CompleteBuyArmourAndAttachInserts Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
