using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseWeaponProficiencyService_Method3 : RMCPResponse
    {
        public List<GR5_WeaponXPLevelInfo> _outXPLevels = new List<GR5_WeaponXPLevelInfo>();

        public RMCPacketResponseWeaponProficiencyService_Method3()
        {
            _outXPLevels.Add(new GR5_WeaponXPLevelInfo());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_outXPLevels.Count);
            foreach (GR5_WeaponXPLevelInfo info in _outXPLevels)
                info.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseWeaponProficiencyService_Method3]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
