using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseInboxMessageService_GetInboxMessagesAfterMessageId : RMCPResponse
    {
        public List<GR5_InboxMessage> Messages {get; set;}

        public RMCPacketResponseInboxMessageService_GetInboxMessagesAfterMessageId()
        {
            Messages = new List<GR5_InboxMessage>();            
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Messages.Count);
            foreach (GR5_InboxMessage msg in Messages)
                msg.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[GetInboxMessagesAfterMessageId Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
