using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_AbilityUpgrade
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
}
