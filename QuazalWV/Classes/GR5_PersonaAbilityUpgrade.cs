using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_PersonaAbilityUpgrade
    {
        public uint AbilityId { get; set; }
        public List<GR5_AbilityUpgradeSlot> Upgrades = new List<GR5_AbilityUpgradeSlot>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, AbilityId);
            Helper.WriteU32(s, (uint)Upgrades.Count);
            foreach (GR5_AbilityUpgradeSlot a in Upgrades)
                a.toBuffer(s);
        }
    }
}
