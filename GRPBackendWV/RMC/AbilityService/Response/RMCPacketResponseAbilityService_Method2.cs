using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAbilityService_Method2 : RMCPacketReply
    {
        public class AbilityUpgradeSlot
        {
            public uint unk1;
            public uint unk2;
            public byte unk3;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU8(s, unk3);
            }
        }
        public class PersonaAbilityUpgrade
        {
            public uint unk1;
            public List<AbilityUpgradeSlot> slots = new List<AbilityUpgradeSlot>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, (uint)slots.Count);
                foreach (AbilityUpgradeSlot a in slots)
                    a.toBuffer(s);
            }
        }

        public List<PersonaAbilityUpgrade> list = new List<PersonaAbilityUpgrade>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (PersonaAbilityUpgrade p in list)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAbilityService_Method2]";
        }
    }
}
