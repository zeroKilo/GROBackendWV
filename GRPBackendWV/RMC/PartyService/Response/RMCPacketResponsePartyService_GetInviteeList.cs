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

        public string unk1 = "";
        public byte[] unk2 = new byte[0x400];
        public List<PartyMember> members = new List<PartyMember>();
        public List<Invitee> invitees = new List<Invitee>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteString(m, unk1);
            Helper.WriteU32(m, (uint)unk2.Length);
            m.Write(unk2, 0, unk2.Length);
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
