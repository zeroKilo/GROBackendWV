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
            public uint id;
            public uint xp;
            public uint level;
            public uint weaponClass;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, id);
                Helper.WriteU32(s, xp);
                Helper.WriteU32(s, level);
                Helper.WriteU32(s, weaponClass);
            }
        }

        public List<WeaponXPLevelInfo> _outXPLevels = new List<WeaponXPLevelInfo>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_outXPLevels.Count);
            foreach (WeaponXPLevelInfo info in _outXPLevels)
                info.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseWeaponProficiencyService_Method3]";
        }
    }
}
