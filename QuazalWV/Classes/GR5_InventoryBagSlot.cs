using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_InventoryBagSlot
    {
        public uint InventoryID;
        public uint SlotID;
        public uint Durability;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, InventoryID);
            Helper.WriteU32(s, SlotID);
            Helper.WriteU32(s, Durability);
        }
    }
}
