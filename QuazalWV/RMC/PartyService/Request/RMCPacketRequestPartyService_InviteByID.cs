using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestPartyService_InviteByID : RMCPRequest
    {
        public List<uint> Pids { get; set; }
        public string InviteMessage { get; set; }
        public uint MatchRequestId { get; set; }
        public uint SessionId { get; set; }
        public uint TeamId { get; set; }

        public RMCPacketRequestPartyService_InviteByID(Stream s)
        {
            Pids = new List<uint>();
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                Pids.Add(Helper.ReadU32(s));
            InviteMessage = Helper.ReadString(s);
            MatchRequestId = Helper.ReadU32(s);
            SessionId = Helper.ReadU32(s);
            TeamId = Helper.ReadU32(s);
        }

        public override string PayloadToString()
        {
            return "";
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Pids.Count);
            foreach(uint pid in Pids)
                Helper.WriteU32(m, pid);
            Helper.WriteString(m, InviteMessage);
            Helper.WriteU32(m, MatchRequestId);
            Helper.WriteU32(m, SessionId);
            Helper.WriteU32(m, TeamId);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[InviteByID Request]";
        }
    }
}
