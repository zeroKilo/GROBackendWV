using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestChatService_JoinPublicChannel : RMCPRequest
    {
        public uint RoomLanguage { get; set; }
        public uint RoomNumber { get; set; }

        public RMCPacketRequestChatService_JoinPublicChannel(Stream s)
        {
            RoomLanguage = Helper.ReadU32(s);
            RoomNumber = Helper.ReadU32(s);
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
            return "[JoinPublicChannel Request]";
        }
    }
}
