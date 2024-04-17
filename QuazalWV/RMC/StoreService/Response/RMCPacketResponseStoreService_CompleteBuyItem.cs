using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_CompleteBuyItem : RMCPResponse
    {
        public List<GR5_UserItem> Inventory { get; set; }

        public RMCPacketResponseStoreService_CompleteBuyItem()
        {
            Inventory = new List<GR5_UserItem>();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Inventory.Count);
            foreach(GR5_UserItem item in Inventory)
                item.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[CompleteBuyItem Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
