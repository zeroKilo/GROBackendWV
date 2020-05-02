using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseServerInfo_Method2 : RMCPResponse
    {
        GR5_TimeInfo info = new GR5_TimeInfo();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            info.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseServerInfo_Method2]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
