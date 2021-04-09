using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAchievementsService_GetPlayerAchievements : RMCPResponse
    {
        public List<GR5_PlayerAchievement> achievements = new List<GR5_PlayerAchievement>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)achievements.Count);
            foreach (GR5_PlayerAchievement a in achievements)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_GetPlayerAchievements]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
