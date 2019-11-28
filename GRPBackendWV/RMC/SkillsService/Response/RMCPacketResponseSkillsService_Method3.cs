using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_Method3 : RMCPacketReply
    {
        public class SkillPower
        {
            public uint m_ID;
            public uint m_ModifierListID;
            public uint m_NameOasisID;
            public uint m_DescriptionOasisID;
            public string m_Name;
            public string m_Description;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ID);
                Helper.WriteU32(s, m_ModifierListID);
                Helper.WriteU32(s, m_NameOasisID);
                Helper.WriteU32(s, m_DescriptionOasisID);
                Helper.WriteString(s, m_Name);
                Helper.WriteString(s, m_Description);
            }
        }

        public List<SkillPower> list = new List<SkillPower>();

        public RMCPacketResponseSkillsService_Method3()
        {
            list.Add(new SkillPower());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (SkillPower sp in list)
                sp.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_Method3]";
        }
    }

}
