using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseFriendsService_GetFriendsList : RMCPResponse
    {
        public List<GR5_FriendData> friends = new List<GR5_FriendData>();

        public RMCPacketResponseFriendsService_GetFriendsList(ClientInfo client)
        {
            friends = DBHelper.GetFriends(client);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)friends.Count());
            foreach (GR5_FriendData fd in friends)
                fd.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseFriendsService_GetFriendsList]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
