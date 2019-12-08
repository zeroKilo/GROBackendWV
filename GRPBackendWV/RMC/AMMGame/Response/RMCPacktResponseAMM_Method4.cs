using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacktResponseAMM_RequestAMMSearch : RMCPacketReply
    {
        public uint count = 1;

        public RMCPacktResponseAMM_RequestAMMSearch()
        {
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, count);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAMM_RequestAMMSearch]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
