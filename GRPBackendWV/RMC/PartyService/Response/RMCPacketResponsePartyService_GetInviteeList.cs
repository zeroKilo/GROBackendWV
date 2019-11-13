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
            public uint unk1;
            public string unk2 = "";
            public byte unk3;
            public uint unk4;
            public uint unk5;
            public uint unk6;
            public byte unk7;
            public byte unk8;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteString(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU8(s, unk7);
                Helper.WriteU8(s, unk8);
            }
        }

        public class Invitee
        {
            public uint unk1;
            public string unk2 = "";
            public byte unk3;
            public uint unk4;
            public uint unk5;
            public uint unk6;
            public byte unk7;
            public byte unk8;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteString(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU8(s, unk7);
                Helper.WriteU8(s, unk8);
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
