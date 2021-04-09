using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePartyService_OnSignInCheckPartyStatus : RMCPResponse
    {
        //implementation might be wrong
        public class Unknown
        {
            public string unk1 = "";
            public GR5_Gathering session = new GR5_Gathering();
            public void toBuffer(Stream s)
            {
                Helper.WriteString(s, unk1);
                MemoryStream m = new MemoryStream(); 
                session.toBuffer(m);
                byte[] buff = m.ToArray();
                Helper.WriteU32(s, (uint)buff.Length + 4);
                Helper.WriteU32(s, (uint)buff.Length);
                s.Write(buff, 0, buff.Length);
            }
        }

        public List<GR5_PartyMember> members = new List<GR5_PartyMember>();
        public List<GR5_Invitee> invitees = new List<GR5_Invitee>();
        public Unknown unk1 = new Unknown();

        public RMCPacketResponsePartyService_OnSignInCheckPartyStatus()
        {
            members.Add(new GR5_PartyMember());
            invitees.Add(new GR5_Invitee());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            unk1.toBuffer(m);
            Helper.WriteU32(m, (uint)members.Count);
            foreach (GR5_PartyMember mem in members)
                mem.toBuffer(m);
            Helper.WriteU32(m, (uint)invitees.Count);
            foreach (GR5_Invitee i in invitees)
                i.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePartyService_OnSignInCheckPartyStatus]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
