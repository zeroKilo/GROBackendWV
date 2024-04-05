using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseInboxMessageService_GetRecentInboxMessages : RMCPResponse
    {
        public List<GR5_InboxMessage> msgs = new List<GR5_InboxMessage>();

        public RMCPacketResponseInboxMessageService_GetRecentInboxMessages()
        {
            // example message (friend request)
            // item messages store item IDs as comma-separated text
            var msg = new GR5_InboxMessage
            {
                Id = 1,
                Unk = 0,
                SenderId = 4661,
                Type = GR5_InboxMessage.TYPE.AddedToFriendList,
                Text = "mimak",
                Date = new QDateTime(DateTime.Now).ToLong(),
                IsNew = 0
            };
            msgs.Add(msg);
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
            return "[RMCPacketResponseInboxMessageService_GetRecentInboxMessages]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
