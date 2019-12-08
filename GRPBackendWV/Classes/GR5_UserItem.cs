using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_UserItem
    {
        public uint InventoryID;
        public uint PersonaID;
        public byte ItemType;
        public uint ItemID;
        public uint OasisName = 70870;
        public float IGCPrice;
        public float GRCashPrice;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, InventoryID);
            Helper.WriteU32(s, PersonaID);
            Helper.WriteU8(s, ItemType);
            Helper.WriteU32(s, ItemID);
            Helper.WriteU32(s, OasisName);
            Helper.WriteFloat(s, IGCPrice);
            Helper.WriteFloat(s, GRCashPrice);
        }
    }
}
