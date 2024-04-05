using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuazalWV
{
    public class RMCPacketRequestInboxMessageService_GetInboxMessagesAfterMessageId : RMCPRequest
    {
        public uint MessageId { get; set; }

        public RMCPacketRequestInboxMessageService_GetInboxMessagesAfterMessageId(Stream s)
        {
            MessageId = Helper.ReadU32(s);
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[MessageId: {MessageId}]");
            return sb.ToString();
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[GetInboxMessagesAfterMessageId Request]";
        }
    }
}
