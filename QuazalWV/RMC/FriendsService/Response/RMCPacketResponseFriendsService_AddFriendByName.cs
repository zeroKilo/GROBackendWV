using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseFriendsService_AddFriendByName : RMCPResponse
    {
        public List<GR5_FriendData> newFriends = new List<GR5_FriendData>();

        public RMCPacketResponseFriendsService_AddFriendByName(ClientInfo client, string friendName)
        {
            //friends = DBHelper.GetFriends(client);
            GR5_FriendData fd = new GR5_FriendData();

            //stub - should be passed friend's ClientInfo and get the info from it
            GR5_BasicPersona friend = new GR5_BasicPersona
            {
                PersonaID = Global.dummyFriendPidCounter++, 
                PersonaName = friendName,
                PersonaStatus = 0x1,
                AvatarPortraitID = 0,
                AvatarDecoratorID = 0,
                AvatarBackgroundColor = 0,
                CurrentCharacterID = 0x1,
                CurrentCharacterLevel = 0x3
            };
            fd.m_Person = friend;
            fd.m_Group = 0;

            if(DBHelper.AddFriend(client, fd))
                newFriends.Add(fd);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)newFriends.Count());
            foreach (GR5_FriendData fd in newFriends)
                fd.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseFriendsService_AddFriendByName]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
