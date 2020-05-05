using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseInboxMessageService_Method2 : RMCPResponse
    {
        public List<GR5_InboxMessage> msgs = new List<GR5_InboxMessage>();

        public RMCPacketResponseInboxMessageService_Method2()
        {
            msgs.Add(new GR5_InboxMessage());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)msgs.Count);
            foreach (GR5_InboxMessage msg in msgs)
                msg.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInboxMessageService_Method2]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
