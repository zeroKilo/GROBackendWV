using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseUnlockService_GetCurrentUserUnlock : RMCPacketReply
    {
        List<GR5_Unlock> _outUnlockList = new List<GR5_Unlock>();

        public RMCPacketResponseUnlockService_GetCurrentUserUnlock()
        {
            _outUnlockList.Add(new GR5_Unlock());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_outUnlockList.Count);
            foreach (GR5_Unlock u in _outUnlockList)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseUnlockService_GetCurrentUserUnlock]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
