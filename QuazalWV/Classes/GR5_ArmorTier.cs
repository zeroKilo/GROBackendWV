using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_ArmorTier
    {
        public uint Id;
        public byte Type;
        public byte Tier;
        public byte ClassID;
        public byte UnlockLevel;
        public byte InsertSlots;
        public uint AssetKey;
        public uint ModifierListId;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, Id);
            Helper.WriteU8(s, Type);
            Helper.WriteU8(s, Tier);
            Helper.WriteU8(s, ClassID);
            Helper.WriteU8(s, UnlockLevel);
            Helper.WriteU8(s, InsertSlots);
            Helper.WriteU32(s, AssetKey);
            Helper.WriteU32(s, ModifierListId);
        }
    }
}
