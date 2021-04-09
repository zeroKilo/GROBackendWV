using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseWeaponProficiencyService_GetPersonaWeaponsXP : RMCPResponse
    {
        public class GR5_PersonaWeaponXP
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

        public List<GR5_PersonaWeaponXP> weaponXPs = new List<GR5_PersonaWeaponXP>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)weaponXPs.Count);
            foreach (GR5_PersonaWeaponXP u in weaponXPs)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseWeaponProficiencyService_GetPersonaWeaponsXP]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
