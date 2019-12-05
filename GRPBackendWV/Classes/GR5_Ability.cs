using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Ability
    {
        public uint Id;
        public byte SlotCount;
        public byte ClassID;
        public byte AbilityType;
        public uint ModifierListId;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, Id);
            Helper.WriteU8(s, SlotCount);
            Helper.WriteU8(s, ClassID);
            Helper.WriteU8(s, AbilityType);
            Helper.WriteU32(s, ModifierListId);
        }
    }
}
