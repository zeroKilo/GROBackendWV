using System.IO;
using System.Collections.Generic;
using QuazalWV.DB;

namespace QuazalWV
{
    public class RMCPacketResponseUnlockService_GetUnlocks : RMCPResponse
    {
        List<GR5_Unlock> Unlocks { get; set; }

        public RMCPacketResponseUnlockService_GetUnlocks()
        {
            Unlocks = UnlockModel.GetUnlocks();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Unlocks.Count);
            foreach (GR5_Unlock u in Unlocks)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[GetUnlocks Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
