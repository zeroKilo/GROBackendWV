using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_GetShoppingDetails : RMCPResponse
    {
        public List<GR5_Coupon> coupons = new List<GR5_Coupon>();
        public List<GR5_SKUModifier> mods = new List<GR5_SKUModifier>();
        public uint lastStoreEntryTime;

        public RMCPacketResponseStoreService_GetShoppingDetails()
        {
            coupons = DBHelper.GetCoupons();
            mods = DBHelper.GetSKUModifiers();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)coupons.Count);
            foreach (GR5_Coupon c in coupons)
                c.toBuffer(m);
            Helper.WriteU32(m, (uint)mods.Count);
            foreach (GR5_SKUModifier md in mods)
                md.toBuffer(m);
            Helper.WriteU32(m, lastStoreEntryTime);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStoreService_GetShoppingDetails]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
