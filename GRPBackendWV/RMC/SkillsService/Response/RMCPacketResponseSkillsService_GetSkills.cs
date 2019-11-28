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
            public uint m_ID;
            public uint m_ParentID;
            public uint m_ParentUnlockLevel;
            public uint m_MaxLevel;
            public uint m_PowerID;
            public uint m_NameOasisID;
            public uint m_DescriptionOasisID;
            public string m_Name;
            public string m_Description;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ID);
                Helper.WriteU32(s, m_ParentID);
                Helper.WriteU32(s, m_ParentUnlockLevel);
                Helper.WriteU32(s, m_MaxLevel);
                Helper.WriteU32(s, m_PowerID);
                Helper.WriteU32(s, m_NameOasisID);
                Helper.WriteU32(s, m_DescriptionOasisID);
                Helper.WriteString(s, m_Name);
                Helper.WriteString(s, m_Description);
            }
        }

        public class SkillUpgrade
        {
            public uint m_ID;
            public uint m_SkillId;
            public uint m_Level;
            public uint m_ModifierListID;
            public uint m_NameOasisID;
            public uint m_DescriptionOasisID;
            public string m_Name;
            public string m_Description;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ID);
                Helper.WriteU32(s, m_SkillId);
                Helper.WriteU32(s, m_Level);
                Helper.WriteU32(s, m_ModifierListID);
                Helper.WriteU32(s, m_NameOasisID);
                Helper.WriteU32(s, m_DescriptionOasisID);
                Helper.WriteString(s, m_Name);
                Helper.WriteString(s, m_Description);
            }
        }

        public List<Skill> skills = new List<Skill>();
        public List<SkillUpgrade> upgrades = new List<SkillUpgrade>();

        public RMCPacketResponseSkillsService_GetSkills()
        {
            skills.Add(new Skill());
            upgrades.Add(new SkillUpgrade());
        }

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
