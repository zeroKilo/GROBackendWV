using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseWeaponProficiencyService_Method3 : RMCPacketReply
    {
        public class WeaponXPLevelInfo
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public uint unk4;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
            }
        }

        public List<WeaponXPLevelInfo> infos = new List<WeaponXPLevelInfo>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)infos.Count);
            foreach (WeaponXPLevelInfo info in infos)
                info.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseWeaponProficiencyService_Method3]";
        }
    }
}
