using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_InitiateBuyItem : RMCPResponse
    {
        public uint TransactionId { get; set; }
        public List<uint> UsedCouponIds { get; set; }

        public RMCPacketResponseStoreService_InitiateBuyItem(uint trId)
        {
            TransactionId = trId;
            UsedCouponIds = new List<uint>();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, TransactionId);
            Helper.WriteU32(m, (uint)UsedCouponIds.Count);
            foreach (uint c in UsedCouponIds)
                Helper.WriteU32(m, c);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[InitiateBuyItem Response]";
        }

        public override string PayloadToString()
        {
            return $"\t[Transaction ID: {TransactionId}]";
        }
    }
}
