using System.Collections.Generic;
using System.IO;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_InitiateBuyAbilityWithUpgrades : RMCPResponse
    {
        public uint TransactionId { get; set; }
        public List<uint> UsedCouponIds { get; set; }

        public RMCPacketResponseStoreService_InitiateBuyAbilityWithUpgrades(uint trId)
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
            return "[InitiateBuyAbilityWithUpgrades Response]";
        }

        public override string PayloadToString()
        {
            return $"\t[Transaction ID: {TransactionId}]";
        }
    }
}
