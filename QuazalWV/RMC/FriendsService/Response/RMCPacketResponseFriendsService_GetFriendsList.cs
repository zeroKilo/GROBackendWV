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
        public List<GR5_FriendData> list = new List<GR5_FriendData>();

        public RMCPacketResponseFriendsService_GetFriendsList()
        {
            list.Add(new GR5_FriendData());
            GR5_FriendData fd = new GR5_FriendData();
            GR5_BasicPersona nonNullFriend = new GR5_BasicPersona
            {
                PersonaID = 0x1235,
                PersonaName = "mimak is ur friend",
                PersonaStatus = 0x11,
                AvatarPortraitID = 0x22222222,
                AvatarDecoratorID = 0x33333333,
                AvatarBackgroundColor = 0x44444444,
                CurrentCharacterID = 0x55,
                CurrentCharacterLevel = 0x66
            };
            fd.m_Person = nonNullFriend;
            list.Add(fd);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count());
            foreach (GR5_FriendData fd in list)
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
