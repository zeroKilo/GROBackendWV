using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseUnlockService_Unlock : RMCPResponse
    {
        public List<GR5_UserUnlockResult> results = new List<GR5_UserUnlockResult>();

        public override byte[] ToBuffer()
        {
            MemoryStream mem = new MemoryStream();
            Helper.WriteU32(mem, (uint)results.Count);
            foreach (GR5_UserUnlockResult result in results)
                result.ToBuffer(mem);
            return mem.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseUnlockService_Unlock]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
