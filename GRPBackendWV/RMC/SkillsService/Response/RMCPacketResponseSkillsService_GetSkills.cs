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
        public class Skill
        {
            public uint[] unk1 = new uint[7];
            public string unk2;
            public string unk3;
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                Helper.WriteString(s, unk2);
                Helper.WriteString(s, unk3);
            }
        }

        public class SkillUpgrade
        {
            public uint[] unk1 = new uint[6];
            public string unk2;
            public string unk3;
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                Helper.WriteString(s, unk2);
                Helper.WriteString(s, unk3);
            }
        }

        public List<Skill> skills = new List<Skill>();
        public List<SkillUpgrade> upgrades = new List<SkillUpgrade>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)skills.Count);
            foreach (Skill s in skills)
                s.toBuffer(m);
            Helper.WriteU32(m, (uint)upgrades.Count);
            foreach (SkillUpgrade u in upgrades)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetSkills]";
        }
    }
}
