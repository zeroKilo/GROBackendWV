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
        public List<GR5_ChatChannelMute> list = new List<GR5_ChatChannelMute>();

        public RMCPacketResponseChatService_Method10()
        {
            list.Add(new GR5_ChatChannelMute());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_ChatChannelMute c in list)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseChatService_Method10]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
