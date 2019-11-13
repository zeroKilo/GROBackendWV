using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseLeaderboardService_GetLeaderboards : RMCPacketReply
    {
        public class Leaderboard
        {
            public uint unk1;
            public string unk2;
            public uint[] unk3 = new uint[6];
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteString(s, unk2);
                foreach (uint u in unk3)
                    Helper.WriteU32(s, u);
            }
        }

        public List<Leaderboard> boards = new List<Leaderboard>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)boards.Count);
            foreach (Leaderboard b in boards)
                b.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLeaderboardService_GetLeaderboards]";
        }
    }
}
