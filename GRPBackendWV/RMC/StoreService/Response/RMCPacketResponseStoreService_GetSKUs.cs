using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStoreService_GetSKUs : RMCPacketReply
    {
        public List<GR5_SKU> skus = new List<GR5_SKU>();

        public RMCPacketResponseStoreService_GetSKUs()
        {
            GR5_SKU s = new GR5_SKU();
            s.m_ItemVector.Add(new GR5_SKUItem());
            skus.Add(s);
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
