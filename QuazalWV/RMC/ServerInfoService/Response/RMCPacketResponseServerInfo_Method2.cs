using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseServerInfo_GetServerLocalTime : RMCPResponse
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
            return "[RMCPacketResponseServerInfo_GetServerLocalTime]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
