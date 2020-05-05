using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_PassiveAbility
    {
        public uint Id;
        public byte ClassID;
        public uint ModifierListID;
        public uint Type;
        public uint AssetKey;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, Id);
            Helper.WriteU8(s, ClassID);
            Helper.WriteU32(s, ModifierListID);
            Helper.WriteU32(s, Type);
            Helper.WriteU32(s, AssetKey);
        }
    }
}
