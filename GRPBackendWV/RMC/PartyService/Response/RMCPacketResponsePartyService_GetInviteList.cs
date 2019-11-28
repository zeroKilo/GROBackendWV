using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePartyService_GetInviteList : RMCPacketReply
    {
        public class Invitation
        {
            public uint PersonaID;
            public string PersonaName;
            public byte PersonaStatus;
            public uint AvatarPortraitID;
            public uint AvatarDecoratorID;
            public uint AvatarBackgroundColor;
            public byte CurrentCharacterID;
            public byte CurrentCharacterLevel;
            public uint m_PartyID;
            public string m_InviterMessage;
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
                Helper.WriteU32(s, m_PartyID);
                Helper.WriteString(s, m_InviterMessage);
            }
        }
        public List<Invitation> _InvitesList = new List<Invitation>();

        public RMCPacketResponsePartyService_GetInviteList()
        {
            _InvitesList.Add(new Invitation());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_InvitesList.Count);
            foreach (Invitation inv in _InvitesList)
                inv.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePartyService_GetInviteList]";
        }
    }
}
