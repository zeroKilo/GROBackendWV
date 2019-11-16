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
            public uint _listIndex;
            public uint weaponID;
            public uint classTypeID;
            public uint weaponType;
            public uint equippableClassTypeID;
            public uint flags;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, weaponID);
                Helper.WriteU32(s, classTypeID);
                Helper.WriteU32(s, weaponType);
                Helper.WriteU32(s, equippableClassTypeID);
                Helper.WriteU32(s, flags);
            }
        }

        public class Unknown1
        {
            public uint _listIndex;
            public List<uint> unk1 = new List<uint>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, (uint)unk1.Count);
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
            }
        }

        public class Component
        {
            public uint _listIndex;
            public uint componentID;
            public uint componentKey;
            public byte componentType;
            public uint boneStructure;
            public uint modifierListID;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, componentID);
                Helper.WriteU32(s, componentKey);
                Helper.WriteU8(s, componentType);
                Helper.WriteU32(s, boneStructure);
                Helper.WriteU32(s, modifierListID);
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
            {
                Helper.WriteU32(m, w._listIndex);
                w.toBuffer(m);
            }
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (Unknown1 u in unk1)
            {
                Helper.WriteU32(m, u._listIndex);
                u.toBuffer(m);
            }
            Helper.WriteU32(m, (uint)unk2.Count);
            foreach (Unknown1 u in unk2)
            {
                Helper.WriteU32(m, u._listIndex);
                u.toBuffer(m);
            }
            Helper.WriteU32(m, (uint)components.Count);
            foreach (Component c in components)
            {
                Helper.WriteU32(m, c._listIndex);
                c.toBuffer(m);
            }
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseWeaponService_GetTemplateWeaponMaps]";
        }
    }
}
