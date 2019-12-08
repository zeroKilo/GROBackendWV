using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_GetSkills : RMCPacketReply
    {
        public List<GR5_Skill> skills = new List<GR5_Skill>();
        public List<GR5_SkillUpgrade> upgrades = new List<GR5_SkillUpgrade>();

        public RMCPacketResponseSkillsService_GetSkills()
        {
            skills.Add(new GR5_Skill());
            upgrades.Add(new GR5_SkillUpgrade());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)skills.Count);
            foreach (GR5_Skill s in skills)
                s.toBuffer(m);
            Helper.WriteU32(m, (uint)upgrades.Count);
            foreach (GR5_SkillUpgrade u in upgrades)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetSkills]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
