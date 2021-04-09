using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseUnlockService_GetUserUnlock : RMCPResponse
    {
        public List<uint> userUnlocks = new List<uint>();

        public RMCPacketResponseUnlockService_GetUserUnlock()
        {
            userUnlocks.Add(0);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)userUnlocks.Count);
            foreach (uint u in userUnlocks)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseUnlockService_GetUserUnlock]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
