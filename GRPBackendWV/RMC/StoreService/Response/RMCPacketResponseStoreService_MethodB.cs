using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStoreService_MethodB : RMCPacketReply
    {
        public List<GR5_Coupon> coupons = new List<GR5_Coupon>();
        public List<GR5_SKUModifier> mods = new List<GR5_SKUModifier>();
        public uint unk1;

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)coupons.Count);
            foreach (GR5_Coupon c in coupons)
                c.toBuffer(m);
            Helper.WriteU32(m, (uint)mods.Count);
            foreach (GR5_SKUModifier md in mods)
                md.toBuffer(m);
            Helper.WriteU32(m, unk1);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStoreService_MethodB]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
