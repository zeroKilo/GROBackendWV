using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAchievementsService_Method2 : RMCPResponse
    {
        public List<GR5_PlayerAchievement> list = new List<GR5_PlayerAchievement>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_PlayerAchievement a in list)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_Method2]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
