using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuazalWV
{
    public class RMCPacketRequestInboxMessageService_SetReadFlags : RMCPRequest
    {
        public List<uint> MessageIds { get; set; }
        public uint Flag { get; set; }

        public RMCPacketRequestInboxMessageService_SetReadFlags(Stream s)
        {
            MessageIds = new List<uint>();
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                MessageIds.Add(Helper.ReadU32(s));
            Flag = Helper.ReadU32(s);
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Msgs: {MessageIds.Count}]");
            sb.AppendLine($"\t[Flag: {Flag}]");
            return sb.ToString();
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[SetReadFlags Request]";
        }
    }
}
