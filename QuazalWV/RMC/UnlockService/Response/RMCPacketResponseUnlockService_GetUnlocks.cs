using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseUnlockService_GetUnlocks : RMCPResponse
    {
        List<GR5_Unlock> _outUnlockList = new List<GR5_Unlock>();

        public RMCPacketResponseUnlockService_GetUnlocks()
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
            return "[RMCPacketResponseUnlockService_GetUnlocks]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
