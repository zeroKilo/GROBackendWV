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

        public RMCPacketResponseAMM_cmn_FetchSessionParticipants(ClientInfo client)
        {
            var participant = new GR5_GameSessionParticipant()
            {
                m_PId = client.PID,
                m_CgId = 1,
                m_SessionId = 1,
                m_TeamId = 1,
                m_Class = 0,
                m_Level = 16,
                m_GhostRank = 8,
                m_Status = 2,
                msz_KeyName = ""
            };
            participants.Add(participant);
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
            return "[RMCPacketResponseAMMDS_cmn_FetchSessionParticipants]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
