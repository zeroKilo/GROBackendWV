using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_AbilityUpgradeSlot
    {
        public uint unk1;
        public uint unk2;
        public byte unk3;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU32(s, unk2);
            Helper.WriteU8(s, unk3);
        }
    }
}
