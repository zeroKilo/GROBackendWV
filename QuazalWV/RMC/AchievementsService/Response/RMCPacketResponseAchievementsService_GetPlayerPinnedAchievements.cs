using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAchievementsService_GetPlayerPinnedAchievements : RMCPResponse
    {
        List<uint> pinnedAchs = new List<uint>();

        public RMCPacketResponseAchievementsService_GetPlayerPinnedAchievements()
        {
            pinnedAchs.Add(0);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)pinnedAchs.Count);
            foreach (uint u in pinnedAchs)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_GetPlayerPinnedAchievements]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
