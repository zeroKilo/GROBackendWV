using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseRegisterEx : RMCPacketReply
    {
        public uint resultCode = 0x00010001;
        public uint connectionId = 78;
        public string clientUrl;
        public RMCPacketResponseRegisterEx(uint pid)
        {
            clientUrl = "prudps:/address=" + Global.serverBindAddress + ";port=3074;CID=1;PID=" + pid + ";sid=1;RVCID=78;stream=3;type=2";
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, resultCode);
            Helper.WriteU32(m, connectionId);
            Helper.WriteString(m, clientUrl);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RegisterEx Response]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Result Code   : 0x" + resultCode.ToString("X8") + "]");
            sb.AppendLine("\t[Connection Id : 0x" + connectionId.ToString("X8") + "]");
            sb.AppendLine("\t[Client Url    : " + clientUrl + "]");
            return sb.ToString();
        }
    }
}