using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestChatService_SetCurrentCharacter : RMCPRequest
    {
        public byte CharId { get; set; }

        public RMCPacketRequestChatService_SetCurrentCharacter(Stream s)
        {
            CharId = Helper.ReadU8(s);
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
            return "[SetCurrentCharacter Request]";
        }
    }
}
