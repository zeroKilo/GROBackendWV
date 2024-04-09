using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAMMGameClientService_LeaveAMMSearch : RMCPResponse
    {
        public uint matchRequestId = 1;

        public RMCPacketResponseAMMGameClientService_LeaveAMMSearch()
        {
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, matchRequestId);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[LeaveAMMSearch Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
