using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_ArmorInsertSlot
    {
        public uint InsertID;
        public uint Durability;
        public byte SlotID;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, InsertID);
            Helper.WriteU32(s, Durability);
            Helper.WriteU8(s, SlotID);
        }
    }
}
