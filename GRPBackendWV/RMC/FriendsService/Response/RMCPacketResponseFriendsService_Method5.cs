using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseFriendsService_Method5 : RMCPacketReply
    {
        public class FriendData
        {
            public class BasicPersona
            {
                public uint PersonaID;
                public string PersonaName;
                public byte PersonaStatus;
                public uint AvatarPortraitID;
                public uint AvatarDecoratorID;
                public uint AvatarBackgroundColor;
                public byte CurrentCharacterID;
                public byte CurrentCharacterLevel;
                public void toBuffer(Stream s)
                {
                    Helper.WriteU32(s, PersonaID);
                    Helper.WriteString(s, PersonaName);
                    Helper.WriteU8(s, PersonaStatus);
                    Helper.WriteU32(s, AvatarPortraitID);
                    Helper.WriteU32(s, AvatarDecoratorID);
                    Helper.WriteU32(s, AvatarBackgroundColor);
                    Helper.WriteU8(s, CurrentCharacterID);
                    Helper.WriteU8(s, CurrentCharacterLevel);
                }
            }
            public BasicPersona m_Person = new BasicPersona();
            public byte m_Group;
            public void toBuffer(Stream s)
            {
                m_Person.toBuffer(s);
                Helper.WriteU8(s, m_Group);
            }
        }

        public List<FriendData> list = new List<FriendData>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count());
            foreach (FriendData fd in list)
                fd.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseFriendsService_Method5]";
        }
    }
}
