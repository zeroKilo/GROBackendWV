using QuazalWV.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestStoreService_InitiateBuyItem : RMCPRequest
    {
        public uint TicketId { get; set; }
        public List<GR5_CartItem> CartItems { get; set; }
        public List<uint> CouponIds { get; set; }

        public RMCPacketRequestStoreService_InitiateBuyItem(Stream s)
        {
            CartItems = new List<GR5_CartItem>();
            CouponIds = new List<uint>();

            TicketId = Helper.ReadU32(s);
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                CartItems.Add(new GR5_CartItem(s));

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
            return "[InitiateBuyItem Request]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Ticket: {TicketId}]");
            sb.AppendLine($"\t[SKU: {CartItems[0].SkuId}]");
            sb.AppendLine($"\t[Currency: {CartItems[0].VirtualCurrencyType}]");
            return sb.ToString();
        }
    }
}