using QuazalWV.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestStoreService_InitiateBuyWeaponAndAttachComponents : RMCPRequest
    {
        public uint TicketId { get; set; }
        public GR5_SingleCartItem WeaponSkuData { get; set; }
        public List<GR5_SingleCartItem> ComponentSkuData { get; set; }
        public List<uint> ComponentInventorySlotIds { get; set; }
        public List<uint> CouponIds { get; set; }

        public RMCPacketRequestStoreService_InitiateBuyWeaponAndAttachComponents(Stream s)
        {
            ComponentSkuData = new List<GR5_SingleCartItem>();
            ComponentInventorySlotIds = new List<uint>();
            CouponIds = new List<uint>();

            TicketId = Helper.ReadU32(s);
            WeaponSkuData = new GR5_SingleCartItem(s);
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                ComponentSkuData.Add(new GR5_SingleCartItem(s));

            count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                ComponentInventorySlotIds.Add(Helper.ReadU32(s));

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
            return "[InitiateBuyWeaponAndAttachComponents Request]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Ticket: {TicketId}]");
            sb.AppendLine($"\t[SKU: {WeaponSkuData.SkuId}]");
            sb.AppendLine($"\t[Currency: {WeaponSkuData.VirtualCurrencyType}]");
            string components = "";
            foreach (var compSlotId in ComponentInventorySlotIds)
                components += $"{compSlotId}, ";
            sb.AppendLine($"\t[Components: {components.Remove(components.Length - 2)}]");
            return sb.ToString();
        }
    }
}