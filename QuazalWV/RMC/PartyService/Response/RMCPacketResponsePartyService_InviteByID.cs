using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePartyService_InviteByID : RMCPResponse
    {
        public List<GR5_Invitee> Invitees = new List<GR5_Invitee>();

        public RMCPacketResponsePartyService_InviteByID(uint pid)
        {
            var example = new GR5_Invitee()
            {
                PersonaID = pid,
                PersonaName = "mimak",
                PersonaStatus = (byte)GR5_BasicPersona.STATUS.Online,
                AvatarPortraitID = 1,
                AvatarDecoratorID = 1,
                AvatarBackgroundColor = 0,
                CurrentCharacterID = 1,
                CurrentCharacterLevel = 7
            };
            Invitees.Add(example);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Invitees.Count);
            foreach (GR5_Invitee inv in Invitees)
                inv.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[InviteByID Request]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
