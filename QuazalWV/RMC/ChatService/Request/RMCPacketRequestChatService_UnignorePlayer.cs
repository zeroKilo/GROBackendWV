using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestChatService_UnignorePlayer : RMCPRequest
    {
        public string Name { get; set; }

        public RMCPacketRequestChatService_UnignorePlayer(Stream s)
        {
            Name = Helper.ReadString(s);
        }

        public override string PayloadToString()
        {
            return "";
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[UnignorePlayer Request]";
        }
    }
}
