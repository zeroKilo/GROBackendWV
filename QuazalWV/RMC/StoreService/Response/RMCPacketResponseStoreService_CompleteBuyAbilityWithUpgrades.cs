using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_CompleteBuyAbilityWithUpgrades : RMCPResponse
    {
        public List<GR5_UserItem> Inventory { get; set; }
        public List<GR5_PersonaAbilityUpgrade> PersonaAbilityUpgrades { get; set; }

        public RMCPacketResponseStoreService_CompleteBuyAbilityWithUpgrades()
        {
            Inventory = new List<GR5_UserItem>();
            PersonaAbilityUpgrades = new List<GR5_PersonaAbilityUpgrade>();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();

            Helper.WriteU32(m, (uint)Inventory.Count);
            foreach (GR5_UserItem item in Inventory)
                item.toBuffer(m);

            Helper.WriteU32(m, (uint)PersonaAbilityUpgrades.Count);
            foreach (GR5_PersonaAbilityUpgrade pat in PersonaAbilityUpgrades)
                pat.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[CompleteBuyAbilityWithUpgrades Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
