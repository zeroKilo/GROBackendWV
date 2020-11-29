using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAMM_cmn_FetchSessionParticipants : RMCPResponse
    {
        public List<GR5_GameSessionParticipant> participants = new List<GR5_GameSessionParticipant>();

        public RMCPacketResponseAMM_cmn_FetchSessionParticipants()
        {
            participants.Add(new GR5_GameSessionParticipant());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)participants.Count);
            foreach (GR5_GameSessionParticipant p in participants)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacktResponseAMMDS_cmn_FetchSessionParticipants]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
