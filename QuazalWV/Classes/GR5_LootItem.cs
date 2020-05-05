using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_LootItem
    {
        public uint mID;
        public uint mLootID;
        public uint mLootItemSku;
        public uint mPercentage;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mID);
            Helper.WriteU32(s, mLootID);
            Helper.WriteU32(s, mLootItemSku);
            Helper.WriteU32(s, mPercentage);
        }
    }
}
