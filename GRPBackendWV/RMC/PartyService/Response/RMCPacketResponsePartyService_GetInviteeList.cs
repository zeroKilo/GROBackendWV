using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePartyService_GetInviteeList : RMCPacketReply
    {
        public class PartyMember
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

        public class Invitee
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

        public class Gathering
        {
            public uint m_idMyself;
            public uint m_pidOwner;
            public uint m_pidHost;
            public ushort m_uiMinParticipants;
            public ushort m_uiMaxParticipants;
            public uint m_uiParticipationPolicy;
            public uint m_uiPolicyArgument;
            public uint m_uiFlags;
            public uint m_uiState;
            public string m_strDescription = "";

            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_idMyself);
                Helper.WriteU32(s, m_pidOwner);
                Helper.WriteU32(s, m_pidHost);
                Helper.WriteU16(s, m_uiMinParticipants);
                Helper.WriteU16(s, m_uiMaxParticipants);
                Helper.WriteU32(s, m_uiParticipationPolicy);
                Helper.WriteU32(s, m_uiPolicyArgument);
                Helper.WriteU32(s, m_uiFlags);
                Helper.WriteU32(s, m_uiState);
                Helper.WriteString(s, m_strDescription);
            }
        }

        public class Unknown
        {
            public string unk1 = "";
            public Gathering unk2 = new Gathering();
            public void toBuffer(Stream s)
            {
                Helper.WriteString(s, unk1);
                MemoryStream m = new MemoryStream(); 
                unk2.toBuffer(m);
                byte[] buff = m.ToArray();
                Helper.WriteU32(s, (uint)buff.Length + 4);
                Helper.WriteU32(s, (uint)buff.Length);
                s.Write(buff, 0, buff.Length);
            }
        }

        public List<PartyMember> members = new List<PartyMember>();
        public List<Invitee> invitees = new List<Invitee>();
        public Unknown unk1 = new Unknown();

        public RMCPacketResponsePartyService_GetInviteeList()
        {
            members.Add(new PartyMember());
            invitees.Add(new Invitee());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            unk1.toBuffer(m);
            Helper.WriteU32(m, (uint)members.Count);
            foreach (PartyMember mem in members)
                mem.toBuffer(m);
            Helper.WriteU32(m, (uint)invitees.Count);
            foreach (Invitee i in invitees)
                i.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePartyService_GetInviteeList]";
        }
    }
}
