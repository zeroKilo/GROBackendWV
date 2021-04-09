using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseInboxMessageService_GetInboxMessageOasisIdDict : RMCPResponse
    {
        public class GR5_InboxMessageOasisId
        {
            public uint unk1;
            public uint unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
            }
        }

        public List<GR5_InboxMessageOasisId> inboxMsgOasisDict = new List<GR5_InboxMessageOasisId>();

        public RMCPacketResponseInboxMessageService_GetInboxMessageOasisIdDict()
        {
            inboxMsgOasisDict.Add(new GR5_InboxMessageOasisId());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)inboxMsgOasisDict.Count);
            foreach (GR5_InboxMessageOasisId u in inboxMsgOasisDict)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInboxMessageService_GetInboxMessageOasisIdDict]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
