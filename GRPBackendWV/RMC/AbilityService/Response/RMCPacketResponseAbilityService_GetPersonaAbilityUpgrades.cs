using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAbilityService_GetPersonaAbilityUpgrades : RMCPacketReply
    {
        public class Ability
        {
            public uint Id;
            public byte SlotCount;
            public byte ClassID;
            public byte AbilityType;
            public uint ModifierListId;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, Id);
                Helper.WriteU8(s, SlotCount);
                Helper.WriteU8(s, ClassID);
                Helper.WriteU8(s, AbilityType); 
                Helper.WriteU32(s, ModifierListId);
            }
        }

        public class AbilityUpgrade
        {
            public uint Id;
            public byte AbilityUpgradeType;
            public byte CompatibleAbilityType;
            public uint ModifierListID;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, Id);
                Helper.WriteU8(s, AbilityUpgradeType);
                Helper.WriteU8(s, CompatibleAbilityType);
                Helper.WriteU32(s, ModifierListID);
            }
        }

        public class PassiveAbility
        {
            public uint Id;
            public byte ClassID;
            public uint ModifierListID;
            public uint Type;
            public uint AssetKey;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, Id);
                Helper.WriteU8(s, ClassID);
                Helper.WriteU32(s, ModifierListID);
                Helper.WriteU32(s, Type);
                Helper.WriteU32(s, AssetKey);
            }
        }

        public List<Ability> abs = new List<Ability>();
        public List<AbilityUpgrade> abups = new List<AbilityUpgrade>();
        public List<PassiveAbility> pabs = new List<PassiveAbility>();

        public RMCPacketResponseAbilityService_GetPersonaAbilityUpgrades()
        {
            abs.Add(new Ability());
            abups.Add(new AbilityUpgrade());
            pabs.Add(new PassiveAbility());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)abs.Count);
            foreach (Ability a in abs)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)abups.Count);
            foreach (AbilityUpgrade a in abups)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)pabs.Count);
            foreach (PassiveAbility a in pabs)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAbilityService_GetPersonaAbilityUpgrades]";
        }
    }
}
