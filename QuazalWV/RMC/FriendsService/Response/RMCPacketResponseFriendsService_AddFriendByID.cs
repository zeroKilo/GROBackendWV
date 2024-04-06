using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseFriendsService_AddFriendByID : RMCPResponse
    {
        public List<GR5_FriendData> friends = new List<GR5_FriendData>();

        public RMCPacketResponseFriendsService_AddFriendByID(ClientInfo client)
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
            return "[AddFriendByID Response]";
        }

        public override string PayloadToString()
        {
            return $"\t[Friends: {friends.Count}]";
        }
    }
}
