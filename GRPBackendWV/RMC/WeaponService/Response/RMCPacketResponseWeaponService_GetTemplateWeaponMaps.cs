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

        public List<GR5_Weapon> weapons = new List<GR5_Weapon>();
        public List<Unknown1> unk1 = new List<Unknown1>();
        public List<Unknown1> unk2 = new List<Unknown1>();
        public List<GR5_Component> components = new List<GR5_Component>();

        public RMCPacketResponseWeaponService_GetTemplateWeaponMaps()
        {
            weapons.Add(new GR5_Weapon());
            unk1.Add(new Unknown1());
            unk2.Add(new Unknown1());
            components.Add(new GR5_Component());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)weapons.Count);
            foreach (GR5_Weapon w in weapons)
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
            foreach (GR5_Component c in components)
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
