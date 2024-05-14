using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuazalWV
{
    public class RMCPacketRequestStoreService_InitiateBuyAbilityWithUpgrades: RMCPRequest
    {
        public uint TicketId { get; set; }
        public GR5_SingleCartItem AbilitySkuData { get; set; }
        public List<GR5_IdSlotPair> UpgradeSKUIdSlots { get; set; }
        public List<GR5_IdSlotPair> UpgradeInventoryIdSlots { get; set; }
        public List<GR5_IdSlotPair> RemoveInventory { get; set; }
        public List<uint> CouponIds { get; set; }

        public RMCPacketRequestStoreService_InitiateBuyAbilityWithUpgrades(Stream s)
        {
            UpgradeSKUIdSlots = new List<GR5_IdSlotPair>();
            UpgradeInventoryIdSlots = new List<GR5_IdSlotPair>();
            RemoveInventory = new List<GR5_IdSlotPair>();
            CouponIds = new List<uint>();

            TicketId = Helper.ReadU32(s);
            AbilitySkuData = new GR5_SingleCartItem(s);
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                UpgradeSKUIdSlots.Add(new GR5_IdSlotPair(s));

            count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                UpgradeInventoryIdSlots.Add(new GR5_IdSlotPair(s));

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
            return "[InitiateBuyAbilityWithUpgrades Request]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Ticket: {TicketId}]");
            sb.AppendLine($"\t[SKU: {AbilitySkuData.SkuId}]");
            sb.AppendLine($"\t[Currency: {AbilitySkuData.VirtualCurrencyType}]");
            return sb.ToString();
        }
    }
}
