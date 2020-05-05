using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_GetSKUs : RMCPResponse
    {
        public List<GR5_SKU> skus = new List<GR5_SKU>();

        public RMCPacketResponseStoreService_GetSKUs()
        {
            skus = DBHelper.GetSKUs();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)skus.Count);
            foreach (GR5_SKU s in skus)
                s.toBuffer(m);
            return m.ToArray(); ;
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStoreService_GetSKUs]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
