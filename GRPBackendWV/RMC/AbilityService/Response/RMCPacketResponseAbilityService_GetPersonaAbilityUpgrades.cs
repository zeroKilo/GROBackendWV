using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAbilityService_GetPersonaAbilityUpgrades : RMCPResponse
    {

        public List<GR5_Ability> abs = new List<GR5_Ability>();
        public List<GR5_AbilityUpgrade> abups = new List<GR5_AbilityUpgrade>();
        public List<GR5_PassiveAbility> pabs = new List<GR5_PassiveAbility>();

        public RMCPacketResponseAbilityService_GetPersonaAbilityUpgrades()
        {
            abs = DBHelper.GetAbilities();
            abups.Add(new GR5_AbilityUpgrade());
            pabs.Add(new GR5_PassiveAbility());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)abs.Count);
            foreach (GR5_Ability a in abs)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)abups.Count);
            foreach (GR5_AbilityUpgrade a in abups)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)pabs.Count);
            foreach (GR5_PassiveAbility a in pabs)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAbilityService_GetPersonaAbilityUpgrades]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
