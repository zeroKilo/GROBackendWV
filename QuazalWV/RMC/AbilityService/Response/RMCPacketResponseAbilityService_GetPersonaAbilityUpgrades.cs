using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAbilityService_GetTemplateAbilities : RMCPResponse
    {

        public List<GR5_Ability> abilities = new List<GR5_Ability>();
        public List<GR5_AbilityUpgrade> upgrades = new List<GR5_AbilityUpgrade>();
        public List<GR5_PassiveAbility> passives = new List<GR5_PassiveAbility>();

        public RMCPacketResponseAbilityService_GetTemplateAbilities()
        {
            abilities = DBHelper.GetAbilities();
            upgrades.Add(new GR5_AbilityUpgrade());
            passives.Add(new GR5_PassiveAbility());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)abilities.Count);
            foreach (GR5_Ability a in abilities)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)upgrades.Count);
            foreach (GR5_AbilityUpgrade a in upgrades)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)passives.Count);
            foreach (GR5_PassiveAbility a in passives)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAbilityService_GetTemplateAbilities]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
