using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseChatService_Method10 : RMCPacketReply
    {
        public class ChatChannelMute
        {
            public uint channel;
            public string reason;
            public uint expiry;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, channel);
                Helper.WriteString(s, reason);
                Helper.WriteU32(s, expiry);
            }
        }

        public List<ChatChannelMute> list = new List<ChatChannelMute>();

        public RMCPacketResponseChatService_Method10()
        {
            list.Add(new ChatChannelMute());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (ChatChannelMute c in list)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseChatService_Method10]";
        }
    }
}
