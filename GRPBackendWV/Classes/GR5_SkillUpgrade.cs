using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_SkillUpgrade
    {
        public uint m_ID;
        public uint m_SkillId;
        public uint m_Level;
        public uint m_ModifierListID;
        public uint m_NameOasisID = 70870;
        public uint m_DescriptionOasisID = 70870;
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
}
