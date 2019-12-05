using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAchievementsService_Method9 : RMCPacketReply
    {
        public List<GR5_Achievement> achs = new List<GR5_Achievement>();
        public List<GR5_AchievementGroup> groups = new List<GR5_AchievementGroup>();
        public List<GR5_AchievementCategory> cats = new List<GR5_AchievementCategory>();

        public RMCPacketResponseAchievementsService_Method9()
        {
            achs.Add(new GR5_Achievement());
            groups.Add(new GR5_AchievementGroup());
            cats.Add(new GR5_AchievementCategory());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)achs.Count());
            foreach (GR5_Achievement a in achs)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)groups.Count());
            foreach (GR5_AchievementGroup g in groups)
                g.toBuffer(m);
            Helper.WriteU32(m, (uint)cats.Count());
            foreach (GR5_AchievementCategory c in cats)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_Method9]";
        }
    }
}
