using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStoreService_EnterCoupons : RMCPacketReply
    {
        public List<GR5_Coupon> coupons = new List<GR5_Coupon>();
        public List<GR5_SKUModifier> mods = new List<GR5_SKUModifier>();

        public RMCPacketResponseStoreService_EnterCoupons()
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
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStoreService_EnterCoupons]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
