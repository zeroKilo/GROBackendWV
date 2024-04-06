using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseChatService_JoinPublicChannel : RMCPResponse
    {
        public GR5_ChatRoom ChatRoom { get; set; }

        public RMCPacketResponseChatService_JoinPublicChannel(uint langId, uint roomNumber)
        {
            ChatRoom = new GR5_ChatRoom()
            {
                RoomLanguage = (GR5_ChatRoom.LANGUAGE)langId,
                RoomNumber = (byte)roomNumber
            };
            ChatRoom.Gathering.m_idMyself = 0x1234;
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            ChatRoom.ToBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[JoinPublicChannel Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
