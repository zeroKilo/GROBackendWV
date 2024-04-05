using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuazalWV
{
    public class RMCPacketRequestInboxMessageService_GetRecentInboxMessages : RMCPRequest
    {
        public uint MessageCount { get; set; }

        public RMCPacketRequestInboxMessageService_GetRecentInboxMessages(Stream s)
        {
            MessageCount = Helper.ReadU32(s);
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Msgs: {MessageCount}]");
            return sb.ToString();
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[GetRecentInboxMessages Request]";
        }
    }
}
