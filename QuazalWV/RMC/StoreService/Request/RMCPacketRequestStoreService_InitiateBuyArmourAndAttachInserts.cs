using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuazalWV
{
    public class RMCPacketRequestStoreService_InitiateBuyArmourAndAttachInserts : RMCPRequest
    {
        public uint TicketId { get; set; }
        public GR5_SingleCartItem ArmorSkuData { get; set; }
        public List<GR5_IdSlotPair> InsertSKUIdSlots { get; set; }
        public List<GR5_IdSlotPair> InsertInventoryIdSlots { get; set; }
        public List<GR5_IdSlotPair> RemoveInventory { get; set; }
        public List<uint> CouponIds { get; set; }

        public RMCPacketRequestStoreService_InitiateBuyArmourAndAttachInserts(Stream s)
        {
            InsertSKUIdSlots = new List<GR5_IdSlotPair>();
            InsertInventoryIdSlots = new List<GR5_IdSlotPair>();
            RemoveInventory = new List<GR5_IdSlotPair>();
            CouponIds = new List<uint>();

            TicketId = Helper.ReadU32(s);
            ArmorSkuData = new GR5_SingleCartItem(s);
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                InsertSKUIdSlots.Add(new GR5_IdSlotPair(s));

            count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                InsertInventoryIdSlots.Add(new GR5_IdSlotPair(s));

            count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                RemoveInventory.Add(new GR5_IdSlotPair(s));

            count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                CouponIds.Add(Helper.ReadU32(s));
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[InitiateBuyArmourAndAttachInserts Request]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Ticket: {TicketId}]");
            sb.AppendLine($"\t[SKU: {ArmorSkuData.SkuId}]");
            sb.AppendLine($"\t[Currency: {ArmorSkuData.VirtualCurrencyType}]");
            return sb.ToString();
        }
    }
}
