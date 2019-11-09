using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketRequestRequestTicket : RMCPacketHeader
    {
        public uint sourcePID;
        public uint targetPID;

        public RMCPacketRequestRequestTicket()
        { 
        }

        public RMCPacketRequestRequestTicket(Stream s)
        {
            sourcePID = Helper.ReadU32(s);
            targetPID = Helper.ReadU32(s);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[RequestTicket Request : PID Source=0x" + sourcePID.ToString("X8") + " PID Target=" + targetPID.ToString("X8") + "]");
            return sb.ToString();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream result = new MemoryStream();
            Helper.WriteU32(result, sourcePID);
            Helper.WriteU32(result, targetPID);
            return result.ToArray();
        }
    }
}
