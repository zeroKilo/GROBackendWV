using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseWeaponService_GetTemplateWeaponMaps : RMCPacketReply
    {
        public class Weapon
        {
            public uint[] unk1 = new uint[6];
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
            }
        }

        public class Unknown1
        {
            public uint unk1;
            public List<uint> unk2 = new List<uint>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, (uint)unk2.Count);
                foreach (uint u in unk2)
                    Helper.WriteU32(s, u);
            }
        }

        public class Component
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public byte unk4;
            public uint unk5;
            public uint unk6;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU8(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
            }
        }

        public List<Weapon> weapons = new List<Weapon>();
        public List<Unknown1> unk1 = new List<Unknown1>();
        public List<Unknown1> unk2 = new List<Unknown1>();
        public List<Component> components = new List<Component>();


        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)weapons.Count);
            foreach (Weapon w in weapons)
                w.toBuffer(m);
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (Unknown1 u in unk1)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk2.Count);
            foreach (Unknown1 u in unk2)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)components.Count);
            foreach (Component c in components)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseWeaponService_GetTemplateWeaponMaps]";
        }
    }
}
