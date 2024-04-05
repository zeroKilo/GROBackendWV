using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuazalWV
{
    public class RMCPacketRequestInboxMessageService_DeleteInboxMessages : RMCPRequest
    {
        public List<uint> MessageIds { get; set; }

        public RMCPacketRequestInboxMessageService_DeleteInboxMessages(Stream s)
        {
            MessageIds = new List<uint>();
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                MessageIds.Add(Helper.ReadU32(s));
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Msgs: {MessageIds.Count}]");
            return sb.ToString();
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[DeleteInboxMessages Request]";
        }
    }
}
