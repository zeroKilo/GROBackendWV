using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAbilityService_Method1 : RMCPacketReply
    {
        public class Ability
        {
            public uint unk1;
            public byte unk2;
            public byte unk3;
            public byte unk4;
            public uint unk5;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU8(s, unk4); 
                Helper.WriteU32(s, unk5);
            }
        }

        public class AbilityUpgrade
        {
            public uint unk1;
            public byte unk2;
            public byte unk3;
            public uint unk4;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU32(s, unk4);
            }
        }

        public class PassiveAbility
        {
            public uint unk1;
            public byte unk2;
            public uint unk3;
            public uint unk4;
            public uint unk5;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
            }
        }

        public List<Ability> abs = new List<Ability>();
        public List<AbilityUpgrade> abups = new List<AbilityUpgrade>();
        public List<PassiveAbility> pabs = new List<PassiveAbility>();

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
            return "[RMCPacketResponseAbilityService_Method1]";
        }
    }
}
