using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_ArmorInsert
    {
        public uint Id;
        public byte Type;
        public uint AssetKey;
        public uint ModifierListID;
        public byte CharacterID;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, Id);
            Helper.WriteU8(s, Type);
            Helper.WriteU32(s, AssetKey);
            Helper.WriteU32(s, ModifierListID);
            Helper.WriteU8(s, CharacterID);
        }
    }
}
