using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_PersonaAbilityUpgrade
    {
        public uint unk1;
        public List<GR5_AbilityUpgradeSlot> slots = new List<GR5_AbilityUpgradeSlot>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU32(s, (uint)slots.Count);
            foreach (GR5_AbilityUpgradeSlot a in slots)
                a.toBuffer(s);
        }
    }
}
